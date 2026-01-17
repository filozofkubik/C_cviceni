def RLE (text):
    if not text:
        return  ""
    vysledek = ""
    x = 1
    pozice = text[0]
    for i in range(1, len(text)):
        if text[i] == pozice:
            x= x+1
        else: 
            vysledek += f"{x}{pozice}"
            pozice = text[i] 
            x = 1

    vysledek += f"{x}{pozice}"
    
    return vysledek
    
vstup = "WWWWWWWWWWWWBWWWWWWWWWWWWBBBWWWWWWWWWWWWWWWWWWWWWWWWBWWWWWWWWWWWWWW"
vystup = RLE(vstup)
print(vystup)