# -*- acsection: general-init -*-
import copy
import random, math
import pygame as pg
import pygamebg

(sirina, visina) = (600, 430)
prozor = pygamebg.open_window(sirina, visina, "Тримини")

# -*- acsection: main -*-

PRAZNO = 4
PUNO = 5
boje = list(map(pg.Color, ["red", "green", "blue", "yellow", "white", "black"]))
        
class Trimino:
    def __init__(self, x, y, d, boja, a, b):
        self.x = x
        self.y = y
        self.kuca_x = x
        self.kuca_y = y
        self.d = d
        self.boja = boja
        self.a = a
        self.b = b

    def crtaj(self):
        for i in range(2):
            for j in range(2):
                if (i, j) != (self.a, self.b):
                    pg.draw.rect(prozor, boje[self.boja], (self.x + i*self.d, self.y + j*self.d, self.d, self.d))
                    pg.draw.rect(prozor, pg.Color("black"), (self.x + i*self.d, self.y + j*self.d, self.d, self.d), 1)

    def sadrzi_tacku(self, x, y):
        return (self.x <= x and x <= self.x + 2*self.d and
                self.y <= y and y <= self.y + 2*self.d)

    def pomeri_kuci(self):
        self.x = self.kuca_x
        self.y = self.kuca_y

class Tabla:
    def __init__(self, dim_table, margina, dim_kvadrata, help = False):
        self.dim_table = dim_table
        self.margina = margina
        self.dim_kvadrata = dim_kvadrata
        self.tabla = [dim_table*[PRAZNO] for i in range(dim_table)]
        (v, k) = (random.randint(0, 7), random.randint(0, 7))
        self.tabla[v][k] = PUNO

    def x(self, i):
        return self.margina+i*self.dim_kvadrata

    def y(self, j):
        return self.margina+j*self.dim_kvadrata
        
    def crtaj(self):
        for i in range(self.dim_table):
            for j in range(self.dim_table):
                if isinstance(self.tabla[i][j], Trimino):
                    boja = self.tabla[i][j].boja
                else:
                    boja = self.tabla[i][j]
                x0 = self.x(i)
                y0 = self.y(j)
                pg.draw.rect(prozor, boje[boja], (x0, y0, self.dim_kvadrata, self.dim_kvadrata))
                pg.draw.rect(prozor, pg.Color("gray"), (x0, y0, self.dim_kvadrata, self.dim_kvadrata), 1)

    def polje(self, x, y):
        v = int(round((x - self.margina) / self.dim_kvadrata))
        k = int(round((y - self.margina) / self.dim_kvadrata))
        return (v, k)

    def u_polju(self, x, y):
        v = int(math.floor((x - self.margina) / self.dim_kvadrata))
        k = int(math.floor((y - self.margina) / self.dim_kvadrata))
        return (v, k)

    def upisi_trimino(self, v, k, trimino):
        if (0 <= v and v <= self.dim_table - 2 and
            0 <= k and k <= self.dim_table - 2):
            for i in range(2):
                for j in range(2):
                    if (i, j) != (trimino.a, trimino.b) and self.tabla[v+i][j+k] != PRAZNO:
                        return False
            trimino_copy = copy.deepcopy(trimino)
            for i in range(2):
                for j in range(2):
                    if (i, j) != (trimino.a, trimino.b):
                        self.tabla[v+i][j+k] = trimino_copy
            return True
        return False

    def na_tabli(self, v, k):
        return (0 <= v and v < self.dim_table and
                0 <= k and k < self.dim_table)
    
    def obrisi_trimino(self, v, k):
        if self.na_tabli(v, k):
            if isinstance(self.tabla[v][k], Trimino):
                trimino = self.tabla[v][k]
                for i in [-1, 0, 1]:
                    for j in [-1, 0, 1]:
                        if self.na_tabli(v+i, k+j) and self.tabla[v+i][k+j] == trimino:
                            self.tabla[v+i][k+j] = PRAZNO
                        
trimini = [Trimino(450, 0, 50, 0,  0, 0),
           Trimino(450, 110, 50, 1, 1, 0),
           Trimino(450, 220, 50, 2, 1, 1),
           Trimino(450, 330, 50, 3, 0, 1)]

tabla = Tabla(8, 15, 50)


prethodno_selektovani = None
def selektovani_trimino(x, y):
    global prethodno_selektovani
    if prethodno_selektovani and prethodno_selektovani.sadrzi_tacku(x, y):
        return prethodno_selektovani
    selektovani = None
    for trimino in trimini:
        if trimino.sadrzi_tacku(x, y):
            selektovani = trimino
            prethodno_selektovani = trimino
            break
    return selektovani


dbclock = pg.time.Clock()

def obradi_dogadjaj(dogadjaj):
    if dogadjaj.type == pg.MOUSEMOTION:
        (x, y) = dogadjaj.pos
        trimino = selektovani_trimino(x, y)
        if trimino and dogadjaj.buttons[0]:
            (dx, dy) = dogadjaj.rel
            trimino.x += dx
            trimino.y += dy
            return True
    elif dogadjaj.type == pg.MOUSEBUTTONDOWN:
        if dbclock.tick() < 500:
            (x, y) = dogadjaj.pos
            trimino = selektovani_trimino(x, y)
            if trimino:
                (v, k) = tabla.polje(trimino.x, trimino.y)
                if tabla.upisi_trimino(v, k, trimino):
                    trimino.pomeri_kuci()
                    return True
            else:
                (v, k) = tabla.u_polju(x, y)
                tabla.obrisi_trimino(v, k)
                return True
    return False
            
def crtaj():
    prozor.fill(pg.Color("white"))

    tabla.crtaj()

    for trimino in trimini:
        trimino.crtaj()

        
# -*- acsection: after-main -*-
pygamebg.event_loop(crtaj, obradi_dogadjaj)
