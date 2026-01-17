import tkinter as tk


okno = tk.Tk()
okno.title("aplikace")
okno.geometry("400x200")
root = tk.Tk()

def slider_zmen(value=None ):

    #nacitani hodnot
    hodnota1 = slider1.get()
    hodnota2 = slider2.get()
    hodnota3 = slider3.get()
    hodnota4 = slider4.get()
    hodnota5 = slider5.get()
    hodnota6 = slider6.get()
    hodnota7 = slider7.get() 
    hodnota8 = slider8.get() 
    hodnota9 = slider9.get() 
    hodnota10 = slider10.get()

    #vyocet rgb z hsv
    hodnota4 = hodnota4 /360
    c = hodnota5 * hodnota6
    x = c * (1 - abs((hodnota4 * 6) % 2 - 1))
    m = hodnota6 - hodnota5
    if 0 <= hodnota4 < 1/6:
        r, g, b = c, x, 0
    elif 1/6 <= hodnota4 < 2/6:
        r, g, b = x, c, 0
    elif 2/6 <= hodnota4 < 3/6:
        r, g, b = 0, c, x
    elif 3/6 <= hodnota4 < 4/6:
        r, g, b = 0, x, c
    elif 4/6 <= hodnota4 < 5/6:
        r, g, b = x, 0, c
    else:
        r, g, b = c, 0, x

    r = round((r + m) * 255)
    g = round((g + m) * 255)
    b = round((b + m) * 255)

    #vypocet rgb z cmyk
    R = 255 * (1-hodnota7) * (1-hodnota10)
    G = 255 * (1-hodnota8) * (1-hodnota10)
    B = 255 * (1-hodnota9) * (1-hodnota10)





    # zmena barvy
    canvas1.config(bg=f'#{int(hodnota1):02x}{int(hodnota2):02x}{int(hodnota3):02x}')
    canvas2.config(bg=f'#{int(r):02x}{int(g):02x}{int(b):02x}')
    canvas3.config(bg=f'#{int(R):02x}{int(G):02x}{int(B):02x}')



    

# RGB slider
slider1 = tk.Scale(okno, from_=0, to=255, orient=tk.HORIZONTAL, command=slider_zmen)

slider1.place(x=50,y = 100, width = 200)
label_slider1 = tk.Label(okno, text="R")
label_slider1.place(x=40, y=120)
slider2 = tk.Scale(okno, from_=0, to=255, orient=tk.HORIZONTAL, command=slider_zmen)

slider2.place(x=50,y = 150, width = 200)
label_slider2 = tk.Label(okno, text="G")
label_slider2.place(x=40, y=170)
slider3 = tk.Scale(okno, from_=0, to=255, orient=tk.HORIZONTAL, command=slider_zmen)

slider3.place(x=50,y = 200, width = 200)       
label_slider3 = tk.Label(okno, text="B")
label_slider3.place(x=40, y=220)














# HSV slider
slider4 = tk.Scale(okno, from_=0, to=359, orient=tk.HORIZONTAL, command=slider_zmen)

slider4.place(x=300,y = 100, width = 200)
label_slider4 = tk.Label(okno, text="H")
label_slider4.place(x=290, y=120)
slider5 = tk.Scale(okno, from_=0, to=1, orient=tk.HORIZONTAL,resolution =0.01, command=slider_zmen)

slider5.place(x=300,y = 150, width = 200)
label_slider5 = tk.Label(okno, text="S")
label_slider5.place(x=290, y=170)
slider6 = tk.Scale(okno, from_=0, to=1, orient=tk.HORIZONTAL, resolution =0.01, command=slider_zmen)

slider6.place(x=300,y = 200, width = 200)
label_slider6 = tk.Label(okno, text="V")
label_slider6.place(x=290, y=220)
slider7 = tk.Scale(okno, from_=0, to=1, orient=tk.HORIZONTAL,resolution =0.01, command=slider_zmen)

slider7.place(x=550,y = 100, width = 200)
label_slider7 = tk.Label(okno, text="C")
label_slider7.place(x=540, y=120)









# CMYK slider
slider8 = tk.Scale(okno, from_=0, to=1, orient=tk.HORIZONTAL,resolution =0.01, command=slider_zmen)

slider8.place(x=550,y = 150, width = 200)
label_slider8 = tk.Label(okno, text="M")
label_slider8.place(x=540, y=170)
slider9 = tk.Scale(okno, from_=0, to=1, orient=tk.HORIZONTAL,resolution =0.01, command=slider_zmen)

slider9.place(x=550,y = 200, width = 200)
label_slider9 = tk.Label(okno, text="Y")
label_slider9.place(x=540, y=220)
slider10 = tk.Scale(okno, from_=0, to=1, orient=tk.HORIZONTAL,resolution =0.01, command=slider_zmen)

slider10.place(x=550,y = 250, width = 200)
label_slider10 = tk.Label(okno, text="K")
label_slider10.place(x=540, y=270)

#inicializace obrazovek
canvas1 = tk.Canvas(okno, width=200, height=200, bg="#000000")
canvas1.place(x=50, y=400)

canvas2 = tk.Canvas(okno, width=200, height=200, bg="#000000")
canvas2.place(x=300, y=400)

canvas3 = tk.Canvas(okno, width=200, height=200, bg="#FFFFFF")
canvas3.place(x=550, y=400)


#hlavní smyčka - okno
okno.mainloop()
