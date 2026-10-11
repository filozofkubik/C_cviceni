import pygame #type:ignore
from sys import exit
import numpy as np #type:ignore
import math

angle=1.5
scalar=0
playerv=1 #rychlost hráče
debug=False
interacttimer=15
soundOn = False
pygame.mixer.init()
music = pygame.mixer.Sound("Sounds\\swing soundtrack.mp3")
music.play(-1)
walkingSfx = pygame.mixer.Sound("sounds\\Walking.mp3")
pygame.init()


pygame.init()
scr_wh=(576,576)   
screen=pygame.display.set_mode((scr_wh)) #tuple vyjadřuje výšku a šířku scalar pxl
pygame.display.set_caption("Saxoheist4D")
mapBaseImage = ["graphics\\Map0_floor.png", "graphics\\Map1_floor.png", "graphics\\Map2_floor.png", "graphics\\Map3_floor.png", "graphics\\Map4_floor.png", "graphics\\Map5_floor.png"]
mapcollisionwallImage = ["graphics\\Map0_middle.png", "graphics\\Map1_middle.png", "graphics\\Map2_middle.png", "graphics\\Map3_middle.png", "graphics\\Map4_middle.png", "graphics\\Map5_middle.png"]
mapwallImage = ["graphics\\Map0_top.png", "graphics\\Map1_top.png", "graphics\\Map2_top.png", "graphics\\Map3_top.png", "graphics\\Map4_top.png", "graphics\\Map5_top.png"]
PlayerPozLevelStart = [(scr_wh[0]*0.5,scr_wh[1]*0.5), (150, 80), (500, 80), (115, 360), (280, 230), (90, 200),]
EndLelelPozStartX = [64, 397, 350, 460, 465, 360]
EndLelelPozStartY = [64, 230, 64, 64, 315, 175]

def color(r, g, b):
    return f"\033[38;2;{r};{g};{b}m"

class mapbase(pygame.sprite.Sprite):
    def __init__(self, cisloMapy):
        super().__init__()
        self.image=pygame.image.load(cisloMapy).convert_alpha() #tady je prom
        self.rect=self.image.get_rect(center=(scr_wh[0]/2,scr_wh[1]/2))
    def update(self):
        screen.blit(self.image, self.rect)

class mapcollisionwall(pygame.sprite.Sprite):
    def __init__(self, cisloMapy):
        super().__init__()
        self.image=pygame.image.load(cisloMapy).convert_alpha()
        self.rect=self.image.get_rect(center=(scr_wh[0]/2,scr_wh[1]/2))
        self.mask=pygame.mask.from_surface(self.image)
    def update(self):
        global scalar
        global angle
        ph = Player_hitbox.sprite #Tady řešíme kolizi s konkrétním hitboxem a setneme angle pohybu opačně k dané stěně
        if self.mask.overlap_area(pygame.mask.Mask((ph.rect_top.width, ph.rect_top.height), fill=True), (ph.rect_top.x - self.rect.x, ph.rect_top.y - self.rect.y)):
            angle = 1.5
        if self.mask.overlap_area(pygame.mask.Mask((ph.rect_bottom.width, ph.rect_bottom.height), fill=True), (ph.rect_bottom.x - self.rect.x, ph.rect_bottom.y - self.rect.y)):
            angle = 0.5
        if self.mask.overlap_area(pygame.mask.Mask((ph.rect_left.width, ph.rect_left.height), fill=True), (ph.rect_left.x - self.rect.x, ph.rect_left.y - self.rect.y)):
            angle = 1
        if self.mask.overlap_area(pygame.mask.Mask((ph.rect_right.width, ph.rect_right.height), fill=True), (ph.rect_right.x - self.rect.x, ph.rect_right.y - self.rect.y)):
            angle = 0
        Map_collisionwall.draw(screen)

class mapwall(pygame.sprite.Sprite):
    def __init__(self, cisloMapy):
        super().__init__()
        self.image=pygame.image.load(cisloMapy).convert_alpha()
        self.rect=self.image.get_rect(center=(scr_wh[0]/2,scr_wh[1]/2))
    def update(self):
        screen.blit(self.image, self.rect)

class playerhitbox(pygame.sprite.Sprite):
    def __init__(self, start_pos):
        super().__init__()
        self.start_pos = start_pos
        self.image=pygame.image.load("graphics\\floor.png").convert_alpha()
        self.image=pygame.transform.scale(self.image,(8,8))
        #vytváří hlavní hitbox
        self.rect = self.image.get_rect(center=self.start_pos)
        #vytváří obdelníky kolem hlavního hitboxu vlevo a vpravo mají prohozenou délku a šířku:
        self.rect_top = pygame.Rect(0, 0, 8, 2) 
        self.rect_top.midbottom = self.rect.midtop

        self.rect_bottom = pygame.Rect(0, 0, 8, 2)
        self.rect_bottom.midtop = self.rect.midbottom

        self.rect_left = pygame.Rect(0, 0, 2, 8)
        self.rect_left.midright = self.rect.midleft

        self.rect_right = pygame.Rect(0, 0, 2, 8)
        self.rect_right.midleft = self.rect.midright
    def player_hitbox_reset_to_start(self):
        self.rect = self.image.get_rect(center=self.start_pos)
        self.rect_top.midbottom = self.rect.midtop
        self.rect_bottom.midtop = self.rect.midbottom
        self.rect_left.midright = self.rect.midleft
        self.rect_right.midleft = self.rect.midright

        if debug: #tohle vykreslí hitboxy je-li zapnutý debug
            pygame.draw.rect(screen, (0, 0, 0), self.rect_top, 1)
            pygame.draw.rect(screen, (0, 0, 0), self.rect_bottom, 1)
            pygame.draw.rect(screen, (0, 0, 0), self.rect_left, 1)
            pygame.draw.rect(screen, (0, 0, 0), self.rect_right, 1)
    
    def update(self, got_caught):
        global debug
        # print(scalar)
        if got_caught:
            self.player_hitbox_reset_to_start()
            return
        prev_center = self.rect.center
        if playerv < 1: #Magie co sem dal Martin
            if (time * playerv) % 2 == 0:
                self.poz_change = (-np.cos(angle * np.pi) * scalar, -np.sin(angle * np.pi) * scalar)
                self.rect.centerx += self.poz_change[0]
                self.rect.centery += self.poz_change[1]
        else:
            self.poz_change = (-np.cos(angle * np.pi) * scalar * playerv, -np.sin(angle * np.pi) * scalar * playerv)
            self.rect.centerx += self.poz_change[0]
            self.rect.centery += self.poz_change[1]

    # Tohle umisťuje pomocný hitboxy kolem hlavního
        self.rect_top.midbottom = self.rect.midtop
        self.rect_bottom.midtop = self.rect.midbottom
        self.rect_left.midright = self.rect.midleft
        self.rect_right.midleft = self.rect.midright
        for wall in Walls:
            if isinstance(wall, Door) and not wall.is_open:
                if wall.mask and wall.mask.overlap_area(pygame.mask.Mask((self.rect.width, self.rect.height), fill=True), 
                                                       (self.rect.x - wall.rect.x, self.rect.y - wall.rect.y)):
                    # Revert to previous position
                    self.rect.center = prev_center
                    self.rect_top.midbottom = self.rect.midtop
                    self.rect_bottom.midtop = self.rect.midbottom
                    self.rect_left.midright = self.rect.midleft
                    self.rect_right.midleft = self.rect.midright
                    break
        if debug: #tohle vykreslí hitboxy je-li zapnutý debug
            pygame.draw.rect(screen, (0, 0, 0), self.rect_top, 1)
            pygame.draw.rect(screen, (0, 0, 0), self.rect_bottom, 1)
            pygame.draw.rect(screen, (0, 0, 0), self.rect_left, 1)
            pygame.draw.rect(screen, (0, 0, 0), self.rect_right, 1)

class player(pygame.sprite.Sprite):
    def __init__(self, start_pos):
        super().__init__()
        self.start_pos = start_pos
        self.image=pygame.image.load("graphics\\player\\Sam-front.png").convert_alpha()
        self.rect=self.image.get_rect(midbottom=(self.start_pos))
        RG_I_0=pygame.image.load("graphics\\player\\player_right_I0.png").convert_alpha()
        RG_I_1=pygame.image.load("graphics\\player\\player_right_I1.png").convert_alpha()
        RG_M_0=pygame.image.load("graphics\\player\\player_right_M0.png").convert_alpha()
        RG_M_1=pygame.image.load("graphics\\player\\player_right_M1.png").convert_alpha()

        UP_I_0=pygame.image.load("graphics\\player\\player_up_I0.png").convert_alpha()
        UP_I_1=pygame.image.load("graphics\\player\\player_up_I1.png").convert_alpha()
        UP_M_0=pygame.image.load("graphics\\player\\player_up_M0.png").convert_alpha()
        UP_M_1=pygame.image.load("graphics\\player\\player_up_M1.png").convert_alpha()

        LF_I_0=pygame.image.load("graphics\\player\\player_left_I0.png").convert_alpha()
        LF_I_1=pygame.image.load("graphics\\player\\player_left_I1.png").convert_alpha()
        LF_M_0=pygame.image.load("graphics\\player\\player_left_M0.png").convert_alpha()
        LF_M_1=pygame.image.load("graphics\\player\\player_left_M1.png").convert_alpha()

        DW_I_0=pygame.image.load("graphics\\player\\player_down_I0.png").convert_alpha()
        DW_I_1=pygame.image.load("graphics\\player\\player_down_I1.png").convert_alpha()
        DW_M_0=pygame.image.load("graphics\\player\\player_down_M0.png").convert_alpha()
        DW_M_1=pygame.image.load("graphics\\player\\player_down_M1.png").convert_alpha()
        
        self.imagelist=[[[LF_I_0,LF_I_1],[LF_M_0,LF_M_1]],
                        [[UP_I_0,UP_I_1],[UP_M_0,UP_M_1]],
                        [[RG_I_0,RG_I_1],[RG_M_0,RG_M_1]],
                        [[DW_I_0, DW_I_1],[DW_M_0, DW_M_1]]]
        self.poz_change=(0,0)
        
    def player_reset_to_start(self):
        self.rect.center = self.start_pos
        self.rect=self.image.get_rect(midbottom=(self.start_pos))
    def update(self, got_caught):
        self.image=self.imagelist[int(angle*2)][int(abs(np.sign(scalar)))][int(time*0.1)%2]
        
        if got_caught:
            self.player_reset_to_start()
            return
        self.rect.midbottom = Player_hitbox.sprite.rect.center

        screen.blit(self.image, self.rect)

        if playerv<1:
            if (time*playerv)%2==0:
                self.poz_change=(-np.cos(angle*np.pi)*scalar,-np.sin(angle*np.pi)*scalar)
                self.rect.centerx+=self.poz_change[0]
                self.rect.centery+=self.poz_change[1]
        else:
            self.poz_change=(-np.cos(angle*np.pi)*scalar*playerv,-np.sin(angle*np.pi)*scalar*playerv)
            self.rect.centerx+=self.poz_change[0]
            self.rect.centery+=self.poz_change[1]
        screen.blit(self.image, self.rect)

class Enemy(pygame.sprite.Sprite):
    def __init__(self, path_points, speed=1):
        super().__init__()
        self.images = [pygame.image.load("graphics\\enemy.png").convert_alpha()]
        self.image = self.images[0]
        self.rect = self.image.get_rect(center=path_points[0])
        self.path = path_points
        self.current_point = 1
        self.speed = speed
        self.time_counter = 0
        
        self.light_angle = math.radians(360)  
        self.light_length = 150  
        self.fixed_direction = pygame.math.Vector2(1, 0)  
        self.direction = pygame.math.Vector2(1, 0)
    def reset_to_start(self):
        """Resetne pozici, index cílového bodu i směr světelného kužele."""
        # Nastavíme nepřítele na první bod
        self.rect.center = self.path[0]
        # Po resetu chceme znovu jít k druhému bodu, proto current_point = 1
        self.current_point = 1
        
        # Volitelně reset animace
        self.time_counter = 0
        self.image = self.images[0]
        
        # Směr kužele můžeme zafixovat na výchozí, např. doprava
        self.direction = pygame.math.Vector2(1, 0)


    def změnasměru(self, new_direction):
        self.fixed_direction = new_direction.normalize()
    def změnauhlu(self, degrees):
        self.light_angle = math.radians(degrees)
    def změnadelky(self, pixels):
        self.light_length = pixels


    def update(self, caught_player):
        if caught_player == True:
            self.reset_to_start()
            return
          
        if not self.path:
            return
        

        target = self.path[self.current_point]
        dx = target[0] - self.rect.centerx
        dy = target[1] - self.rect.centery
        dist = np.hypot(dx, dy)
        
        if dist < self.speed:
            self.current_point = (self.current_point + 1) % len(self.path)
            next_target = self.path[self.current_point]
            direction_vector = pygame.math.Vector2(next_target[0] - self.rect.centerx, next_target[1] - self.rect.centery)
            if direction_vector.length() != 0:
                self.direction = direction_vector.normalize()
        else:
            dx, dy = dx / dist, dy / dist
            self.rect.centerx += dx * self.speed
            self.rect.centery += dy * self.speed
            self.direction = self.fixed_direction
        
        self.time_counter += 1
        self.image = self.images[(self.time_counter // 30) % len(self.images)]
        
        screen.blit(self.image, self.rect)
        self.draw_light_cone()
        self.check_player_in_light()

    def draw_light_cone(self):
        start_pos = pygame.math.Vector2(self.rect.center)


        rays = []
        ray_count = 30  

        for i in range(ray_count + 1):
            angle_offset = -self.light_angle / 2 + (self.light_angle * i / ray_count)
            direction = self.direction.rotate_rad(angle_offset)
            end_point = self.cast_ray(start_pos, direction)
            rays.append(end_point)

       
        s = pygame.Surface(screen.get_size(), pygame.SRCALPHA)
        pygame.draw.polygon(s, (255, 255, 0, 100), [start_pos] + rays)
        screen.blit(s, (0, 0))

    def cast_ray(self, start_pos, direction):
        max_length = self.light_length
        step = 2

        for i in range(0, int(max_length), step):
            check_pos = start_pos + direction * i
            x, y = int(check_pos.x), int(check_pos.y)

            rel_x = x - Map_collisionwall.sprite.rect.left
            rel_y = y - Map_collisionwall.sprite.rect.top

            # Přidáme kontrolu hranic
            if 0 <= rel_x < Map_collisionwall.sprite.mask.get_size()[0] and \
            0 <= rel_y < Map_collisionwall.sprite.mask.get_size()[1]:

                if Map_collisionwall.sprite.mask.get_at((rel_x, rel_y)):
                    return (x, y)
            else:
                break

        return (int(start_pos.x + direction.x * max_length),
                int(start_pos.y + direction.y * max_length))

    def check_player_in_light(self):
        player_pos = pygame.math.Vector2(Player_hitbox.sprite.rect.center)
        enemy_pos = pygame.math.Vector2(self.rect.center)
        vec_to_player = player_pos - enemy_pos

        if vec_to_player.length() > self.light_length:
            return

        vec_to_player_norm = vec_to_player.normalize()
        angle_between = self.direction.angle_to(vec_to_player_norm)

        if abs(angle_between) <= math.degrees(self.light_angle / 2):
            
            hit_point = self.cast_ray(enemy_pos, vec_to_player_norm)
            if (player_pos - enemy_pos).length() <= (pygame.math.Vector2(hit_point) - enemy_pos).length():
                print("Prohra! Hráč je v kuželu světla a není za zdí!")
                return True

class ReactivePlace:
    def __init__(self, x, y, sirka, vyska):
        self.rect = pygame.Rect(x, y, sirka, vyska)
        
        self.image = pygame.image.load("graphics\\door.png").convert_alpha()

    def zkontroluj_kolizi(self, hrac_rect):
        
        if self.rect.colliderect(hrac_rect):
            return True
        else:
            return False
   
    def vykresli(self):
        """Vykreslí místo na obrazovku."""
        screen.blit(self.image, self.rect)

class Button():  
    def __init__(self, image, pos, image_new):
        self.image = image
        self.x_pos = pos[0]
        self.y_pos = pos[1]
        self.image_new = image_new
        self.rect = self.image.get_rect(center=(self.x_pos, self.y_pos))
    
    def update(self, screen):
        if self.image is not None:
            screen.blit(self.image, self.rect)

    def checkForInput(self, position):
        if position[0] in range(self.rect.left, self.rect.right) and position[1] in range(self.rect.top, self.rect.bottom):
            return True
        return False

    def changeColor(self, position):
        if position[0] in range(self.rect.left, self.rect.right) and position[1] in range(self.rect.top, self.rect.bottom):
            self.image = self.image_new
        else:
            self.image = self.image
class Door(pygame.sprite.Sprite):
    def __init__(self, pos, size, color_closed=(150, 75, 0), color_open=(0, 255, 0)):
        super().__init__()
        self.color_closed = color_closed
        self.color_open = color_open
        self.image = pygame.Surface(size)
        self.image.fill(self.color_closed)
        self.rect = self.image.get_rect(topleft=pos)
        self.mask = pygame.mask.from_surface(self.image)
        self.is_open = False

    def open(self):
        self.is_open = True
        self.image.fill(self.color_open)
        self.mask = None

    def close(self):
        global angle
        self.is_open = False
        self.image.fill(self.color_closed)
        self.mask = pygame.mask.from_surface(self.image)
        ph = Player_hitbox.sprite
        if self.mask.overlap_area(pygame.mask.Mask((ph.rect_top.width, ph.rect_top.height), fill=True), (ph.rect_top.x - self.rect.x, ph.rect_top.y - self.rect.y)):
            angle = 1.5
        if self.mask.overlap_area(pygame.mask.Mask((ph.rect_bottom.width, ph.rect_bottom.height), fill=True), (ph.rect_bottom.x - self.rect.x, ph.rect_bottom.y - self.rect.y)):
            angle = 0.5
        if self.mask.overlap_area(pygame.mask.Mask((ph.rect_left.width, ph.rect_left.height), fill=True), (ph.rect_left.x - self.rect.x, ph.rect_left.y - self.rect.y)):
            angle = 1
        if self.mask.overlap_area(pygame.mask.Mask((ph.rect_right.width, ph.rect_right.height), fill=True), (ph.rect_right.x - self.rect.x, ph.rect_right.y - self.rect.y)):
            angle = 0
    

playerpoz=(scr_wh[0]*0.5,scr_wh[1]*0.5)
Player_object=pygame.sprite.GroupSingle()
Player_object.add(player((playerpoz))) # startovní pozice hráče
Player_hitbox=pygame.sprite.GroupSingle()
Player_hitbox.add(playerhitbox(playerpoz))
Map_base=pygame.sprite.GroupSingle()   
Map_wall=pygame.sprite.GroupSingle()
Map_collisionwall=pygame.sprite.GroupSingle()
Map_base.add(mapbase(mapBaseImage[0]))
Map_collisionwall.add(mapcollisionwall(mapcollisionwallImage[0]))
Map_wall.add(mapwall(mapwallImage[0]))
Walls = pygame.sprite.Group()
   

Enemies = pygame.sprite.Group()
#enemy_path = [(-500, -150), (-400, -150)]  
#enemy = Enemy(enemy_path, speed=1.2)
#enemy.změnasměru(pygame.math.Vector2(0, -1))######################################################################
#enemy.změnadelky(90)
#enemy.změnauhlu(60)
#Enemies.add(enemy)
reactive_place = ReactivePlace(64, 64, 40, 40)
time=0

floor= pygame.image.load("graphics\\floor.png")
floor=pygame.transform.scale(floor,(scr_wh))

door = Door((200, 300), (50, 100))
#Walls.add(door)




background = pygame.image.load("graphics\\background.png")
background_levels = pygame.image.load("graphics\\background_levels.png")
background_dead = pygame.image.load("graphics\\background_dead.png")
def dead():
    global game_active
    pygame.display.set_caption('YOU GOT CAUGHT - Saxoheist4D')
    
    while True:
        
        game_active = False
        screen.blit(background_dead, (0, 0))
        
        DEAD_MOUSE_POS = pygame.mouse.get_pos()

        DEAD_BACK = Button(image=pygame.image.load("graphics\\buttons\\back_button.png"), pos=(288, 460), image_new=pygame.image.load("graphics\\buttons\\back_button_new.png"))
                            

        DEAD_BACK.changeColor(DEAD_MOUSE_POS)
        DEAD_BACK.update(screen)

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                exit()
            if event.type == pygame.MOUSEBUTTONDOWN:
                if DEAD_BACK.checkForInput(DEAD_MOUSE_POS):
                    return
        pygame.display.update()

def levels():
    pygame.display.set_caption('LEVELS - Saxoheist4D')
    while True:
        
        screen.blit(background_levels, (0, 0))
        
        LEVELS_MOUSE_POS = pygame.mouse.get_pos()

        LEVELS_BACK = Button(image=pygame.image.load("graphics\\buttons\\back_button.png"), pos=(288, 460), image_new=pygame.image.load("graphics\\buttons\\back_button_new.png"))
                            

        LEVELS_BACK.changeColor(LEVELS_MOUSE_POS)
        LEVELS_BACK.update(screen)

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                exit()
            if event.type == pygame.MOUSEBUTTONDOWN:
                if LEVELS_BACK.checkForInput(LEVELS_MOUSE_POS):
                    menu()
        pygame.display.update()
def setup_level(level_number):
    Walls.empty()
    Enemies.empty()
    if level_number == 1:
        enemy = Enemy([(100, 120), (300, 120)], speed=1.2)
        enemy.změnasměru(pygame.math.Vector2(1, 0))
        enemy.změnadelky(100)
        enemy.změnauhlu(60)
        Enemies.add(enemy)

    elif level_number == 2:
        enemy = Enemy([(80, 70), (80, 270),], speed=1.2)
        enemy.změnasměru(pygame.math.Vector2(1, 0))
        enemy.změnadelky(100)
        enemy.změnauhlu(60)
        Enemies.add(enemy)
        enemy1 = Enemy([(270, 270), (80, 270),], speed=1.2)
        enemy1.změnasměru(pygame.math.Vector2(0, -1))
        enemy1.změnadelky(100)
        enemy1.změnauhlu(60)
        Enemies.add(enemy1)
        enemy2 = Enemy([(500, 70), (500, 300),], speed=1.2)
        enemy2.změnasměru(pygame.math.Vector2(-1, 0))
        enemy2.změnadelky(100)
        enemy2.změnauhlu(60)
        Enemies.add(enemy2)
        enemy3 = Enemy([(300, 70), (500, 70),], speed=1.2)
        enemy3.změnasměru(pygame.math.Vector2(0, 1))
        enemy3.změnadelky(100)
        enemy3.změnauhlu(60)
        Enemies.add(enemy3)

    elif level_number == 3:
        enemy = Enemy([(50, 100), (230, 100)], speed=1.2)
        enemy.změnasměru(pygame.math.Vector2(1, 0))
        enemy.změnadelky(100)
        enemy.změnauhlu(60)
        Enemies.add(enemy)
        enemy1 = Enemy([(400, 270), (80, 270),], speed=1.5)
        enemy1.změnasměru(pygame.math.Vector2(0, -1))
        enemy1.změnadelky(150)
        enemy1.změnauhlu(60)
        Enemies.add(enemy1)
    elif level_number == 4:
        enemy = Enemy([(100, 100), (400, 100)], speed=1.2)
        enemy.změnasměru(pygame.math.Vector2(1, 0))
        enemy.změnadelky(100)
        enemy.změnauhlu(60)
        Enemies.add(enemy)
        enemy2 = Enemy([(500, 70), (500, 300),], speed=1.2)
        enemy2.změnasměru(pygame.math.Vector2(0, -1))
        enemy2.změnadelky(100)
        enemy2.změnauhlu(60)
        Enemies.add(enemy2)
        enemy3 = Enemy([(295, 270),(325, 270)], speed=1.2)
        enemy3.změnasměru(pygame.math.Vector2(0, 1))
        enemy3.změnadelky(100)
        enemy3.změnauhlu(60)
        Enemies.add(enemy3)       
    elif level_number == 5:
        enemy = Enemy([(100, 100), (400, 100)], speed=1.5)
        enemy.změnasměru(pygame.math.Vector2(1, 0))
        enemy.změnadelky(100)
        enemy.změnauhlu(60)
        Enemies.add(enemy)
        enemy1 = Enemy([(80, 310), (100, 310),], speed=1.2)
        enemy1.změnasměru(pygame.math.Vector2(1, 0))
        enemy1.změnadelky(300)
        enemy1.změnauhlu(1)
        Enemies.add(enemy1)
        enemy2 = Enemy([(365, 310), (370, 310),], speed=1.2)
        enemy2.změnasměru(pygame.math.Vector2(0, -1))
        enemy2.změnadelky(150)
        enemy2.změnauhlu(1)
        Enemies.add(enemy2)
    elif level_number == 6:
        enemy = Enemy([(100, 100), (400, 100)], speed=1.2)
        enemy.změnasměru(pygame.math.Vector2(1, 0))
        enemy.změnadelky(100)
        enemy.změnauhlu(60)
        Enemies.add(enemy)
        enemy2 = Enemy([(333, 300), (344, 330),], speed=1.2)
        enemy2.změnasměru(pygame.math.Vector2(0, -1))
        enemy2.změnadelky(100)
        enemy2.změnauhlu(30)
        Enemies.add(enemy2)  

def play_loop(): # herní smyčka
    global game_active, time, scalar, angle, interacttimer, debug, soundOn
    global playerpoz, Player_object, Player_hitbox, Map_base, Map_wall,  Map_collisionwall, reactive_place
    LevelNumber = 1
    setup_level(LevelNumber)
    game_active = True
    while game_active:   
        if reactive_place.zkontroluj_kolizi(Player_hitbox.sprite.rect) == True:
            
            playerpoz=(PlayerPozLevelStart[LevelNumber])
            
            Player_object=pygame.sprite.GroupSingle()
            Player_object.add(player((playerpoz))) # startovní pozice hráče
            
            Player_hitbox=pygame.sprite.GroupSingle()
            Player_hitbox.add(playerhitbox(playerpoz))
            
            Map_base=pygame.sprite.GroupSingle()   
            
            Map_wall=pygame.sprite.GroupSingle()
            
            Map_collisionwall=pygame.sprite.GroupSingle()
            Map_base.add(mapbase(mapBaseImage[LevelNumber]))
            Map_collisionwall.add(mapcollisionwall(mapcollisionwallImage[LevelNumber]))
            Map_wall.add(mapwall(mapwallImage[LevelNumber]))
            setup_level(LevelNumber)
            Enemies.empty()
            
            reactive_place = ReactivePlace(EndLelelPozStartX[LevelNumber], EndLelelPozStartY[LevelNumber], 40, 40)
            LevelNumber += 1
            setup_level(LevelNumber)
            
        for event in pygame.event.get():
            if event.type== pygame.QUIT:
                pygame.quit()
                exit()
        
        time+=1
        keys=pygame.key.get_pressed()
        if interacttimer>0:
            interacttimer-=1
        elif keys[pygame.K_p]:
            interacttimer=15
            if debug==True:
                debug=False
            else:
                debug=True
        if keys[pygame.K_w] and keys[pygame.K_d]:
            scalar=1
            angle=0.75
        elif keys[pygame.K_w] and keys[pygame.K_a]:
            scalar=1
            angle=0.25
        elif keys[pygame.K_s] and keys[pygame.K_d]:
            scalar=1
            angle=1.25
        elif keys[pygame.K_s] and keys[pygame.K_a]:
            scalar=1
            angle=1.75

        elif keys[pygame.K_w]:
            scalar=1
            angle=0.5
        elif keys[pygame.K_d]:
            scalar=1
            angle=1
        elif keys[pygame.K_s]:
            scalar=1
            angle=1.5
        elif keys[pygame.K_a]:
            scalar=1
            angle=0

        screen.blit(floor,(0,0))
        Map_base.update()
        Map_collisionwall.update()
        Player_object.update(False)
        Map_wall.update()
        keys = pygame.key.get_pressed()
        #if keys[pygame.K_e]:
            #door.open()
        #else:
            #door.close()
        Walls.draw(screen)
        Player_hitbox.update(False)
        
        if abs(scalar)>0:
            scalar-=0.5*np.sign(scalar)
        keys = pygame.key.get_pressed()
        if keys[pygame.K_w] or keys[pygame.K_a] or keys[pygame.K_s] or keys[pygame.K_d]:
            if not soundOn:
                walkingSfx.play(-1)  # Loop indefinitely
                soundOn = True
        else:
            if soundOn:
                walkingSfx.stop()
                soundOn = False

        # --- UPDATE A VYKRESLENÍ NEPŘÁTEL ---
        Enemies.update(False)
        # už nepoužíváme Enemies.draw(screen), protože každý enemy vykresluje sám ve svém update()

        # --- KOLIZE NEPŘÍTEL S HRÁČEM (DETEKCE) ---
        for enemy in Enemies:
            if enemy.rect.colliderect(Player_hitbox.sprite.rect) or enemy.check_player_in_light() == True :
                print('byl jsi chycen')
                Enemies.update(True)
                Player_object.update(True)
                Player_hitbox.update(True)
                dead()
                return
                        
        
        reactive_place.vykresli()
        pygame.display.update()
        
    

def menu():
    pygame.display.set_caption('MENU - Saxoheist4D')
    while True:
        screen.blit(background, (0, 0))

        MENU_MOUSE_POS = pygame.mouse.get_pos()

    
        PLAY_BUTTON = Button(image=pygame.image.load("graphics\\buttons\\play_button.png"), pos=(288, 220), image_new=pygame.image.load("graphics\\buttons\\play_button_new.png")) 
                        
        LEVELS_BUTTON = Button(image=pygame.image.load("graphics\\buttons\\levels_button.png"), pos=(288, 350), image_new=pygame.image.load("graphics\\buttons\\levels_button_new.png")) 
                            
        QUIT_BUTTON = Button(image=pygame.image.load("graphics\\buttons\\quit_button.png"), pos=(288, 480), image_new=pygame.image.load("graphics\\buttons\\quit_button_new.png")) 
                           


        for button in [PLAY_BUTTON, LEVELS_BUTTON, QUIT_BUTTON]:
            button.changeColor(MENU_MOUSE_POS)
            button.update(screen)
        
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                exit()
            if event.type == pygame.MOUSEBUTTONDOWN:
                if PLAY_BUTTON.checkForInput(MENU_MOUSE_POS):
                    play_loop()
                if LEVELS_BUTTON.checkForInput(MENU_MOUSE_POS):
                    levels()
                if QUIT_BUTTON.checkForInput(MENU_MOUSE_POS):
                    pygame.quit()
                    exit()

        pygame.display.update()
              
menu()