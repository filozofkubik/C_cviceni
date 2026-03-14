def spravnecislo(n: int) -> bool:
    return n != 0


def soucetcifer_text(text: str) -> int:
    return sum(int(c) for c in text if c.isdigit())
def prikladynajdi(rada: str):
    priklady = []
    skore = 0
    delka = len(rada)
    for start in range(delka):
        pnvr = False
        for i in range(start + 1, delka):
            if pnvr:
                break
            a = int(rada[start:i])
            if not spravnecislo(a):
                continue
            if i - start == 1:
                for mocneni in (2, 3):
                    vysledek = a ** mocneni
                    vysl_str = str(vysledek)
                    if rada.startswith(vysl_str, i):
                        zapis = f"{a}^{mocneni}={vysledek}"
                        priklady.append(zapis)
                        skore += soucetcifer_text(zapis)
                        pnvr = True
                        break
            for j in range(i + 1, delka + 1):
                if pnvr:
                    break
                b = int(rada[i:j])
                if not spravnecislo(b):
                    continue
                zbytek = rada[j:]
                operace = [
                    (a + b, "+"),
                    (a - b, "-"),
                    (a * b, "*"),
                ]
                if b != 0 and a % b == 0:
                    operace.append((a // b, "/"))
                for vysl, op in operace:
                    if not spravnecislo(vysl):
                        continue
                    if zbytek.startswith(str(vysl)):
                        zapis = f"{a}{op}{b}={vysl}"
                        priklady.append(zapis)
                        skore += soucetcifer_text(zapis)
                        pnvr = True
                        break
    return priklady, skore
inputrada = input("zadejcislo: ").strip()
priklady, skore = prikladynajdi(inputrada)
for p in priklady:
    print(p)
print("Skóre:", skore)