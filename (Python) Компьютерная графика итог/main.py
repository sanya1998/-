import os
import matplotlib.pyplot as plt
import PIL.Image as pi

from core import parsing_file
from visualize import draw_faces
from transformation import *


class Object:
    def __init__(self):
        self.path = ''
        self.name = 'face.obj'
        self.path_to_file = os.path.join(self.path, self.name)

        self.pic_size = 512

        self.lst_v = [[], [], [], []]
        self.lst_vt = [[], [], []]
        self.lst_vn = [[], [], [], []]
        self.lst_f = [[[], [], []], [[], [], []], [[], [], []]]

        parsing_file(self.path_to_file, self.lst_v, self.lst_vt, self.lst_vn, self.lst_f)

        # Угол, на который будет повернут объект
        self.angle_rotation_object = 0

        # Свет
        self.L = [0, 250, 250]
        # Угол, на который повернут свет
        self.angle_rotation_light = 0

        self.V = [0, 0, -1]

        # Подгружаем текстуру
        self.img_texture = pi.open("african_head_diffuse.tga")
        self.img_np_texture = np.array(self.img_texture)

        self.img = np.zeros((self.pic_size, self.pic_size, 3), dtype=np.uint8)

        # Поворот света
        rotation_point(self.angle_rotation_light, "y", self.L)

        # Переход от локальной к глобальной
        # Масштабирование (сжатие, растяжение)(Пока что умножение на единичную матрицу (M1) (коэф от 0 до 1)
        compression(1, 1, 1, self.lst_v)
        compression(1, 1, 1, self.lst_vn, "for_normal")

        # Поворот головы, ("y" - ось вращения)
        rotation(self.angle_rotation_object, "y", self.lst_v)
        rotation(self.angle_rotation_object, "y", self.lst_vn, "for_normal")

        # Переход в СК камеры...(M2), чтобы объект в параллелепипед попал (здесь уже меняется 4ая координата)
        multiplication_M2_on(self.lst_v, self.pic_size)
        multiplication_M2_on(self.lst_vn, self.pic_size, "for_normal")

        # Делим на 4ую координату и попадаем в кубик
        # delenie_on_4_coord(self.lst_v)
        # delenie_on_4_coord(self.lst_vn)

        # Проецируем на ViewPort
        projection(self.lst_v, self.pic_size)
        # projection(self.lst_vn, self.pic_size)

        # Ортографическая проекция (M3)
        #multiplication_M3_on(self.lst_v, self.pic_size)

        self.img = draw_faces(self.lst_v, self.lst_vt, self.lst_vn, self.lst_f, self.pic_size, self.img, self.L, self.V,
                              self.img_np_texture)
        plt.imshow(self.img)
        plt.show()
        return


object = Object()
