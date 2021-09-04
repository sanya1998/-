import numpy as np


# Сжатие
def compression(a, b, c, lst_v, dly_chego=""):
    matrica = [[a, 0, 0, 0], [0, b, 0, 0], [0, 0, c, 0], [0, 0, 0, 1]]

    if dly_chego == "for_normal":
        matrica = np.linalg.inv(matrica)
        matrica = np.transpose(matrica)

    multiplication(matrica, lst_v)
    return


# Поворот
def rotation(a, os_xyz, lst_v, dly_chego=""):
    # Градусы в радианы:
    a = a*np.pi/180.0

    sdvig_x = (max(lst_v[0]) + min(lst_v[0])) / 2
    sdvig_y = (max(lst_v[1]) + min(lst_v[1])) / 2
    sdvig_z = (max(lst_v[2]) + min(lst_v[2])) / 2

    if os_xyz == "x":
        matrica_rotation = [[1, 0, 0, 0], [0, np.cos(a), -np.sin(a), 0], [0, np.sin(a), np.cos(a), 0], [0, 0, 0, 1]]
    elif os_xyz == "y":
        matrica_rotation = [[np.cos(a), 0, np.sin(a), 0], [0, 1, 0, 0], [-np.sin(a), 0, np.cos(a), 0], [0, 0, 0, 1]]
    elif os_xyz == "z":
        matrica_rotation = [[np.cos(a), -np.sin(a), 0, 0], [np.sin(a), np.cos(a), 0, 0], [0, 0, 1, 0], [0, 0, 0, 1]]

    if dly_chego == "for_normal":
        matrica_rotation = np.linalg.inv(matrica_rotation)
        matrica_rotation = np.transpose(matrica_rotation)

    # Сначала двигаем центр тела в начало системы координат
    transfer(-sdvig_x, -sdvig_y, -sdvig_z, lst_v, dly_chego)

    # Вращаем
    multiplication(matrica_rotation, lst_v)

    sdvig_x = (max(lst_v[0]) + min(lst_v[0])) / 2
    sdvig_y = (max(lst_v[1]) + min(lst_v[1])) / 2
    sdvig_z = (max(lst_v[2]) + min(lst_v[2])) / 2

    # Возвращаем в исходное положение
    transfer(sdvig_x, sdvig_y, sdvig_z, lst_v, dly_chego)
    return


# Поворот точки
def rotation_point(a, os_xyz, L):
    # Градусы в радианы:
    a = a * np.pi / 180.0

    if os_xyz == "x":
        matrica_rotation = [[1, 0, 0], [0, np.cos(a), -np.sin(a)], [0, np.sin(a), np.cos(a)], [0, 0, 0]]
    elif os_xyz == "y":
        matrica_rotation = [[np.cos(a), 0, np.sin(a)], [0, 1, 0], [-np.sin(a), 0, np.cos(a)], [0, 0, 0]]
    elif os_xyz == "z":
        matrica_rotation = [[np.cos(a), -np.sin(a), 0], [np.sin(a), np.cos(a), 0], [0, 0, 1], [0, 0, 0]]

    res = np.dot(matrica_rotation, L)

    L[0] = round(res[0], 3)
    L[1] = round(res[1], 3)
    L[2] = round(res[2], 3)

# Перенос
def transfer(dx, dy, dz, lst_v, dly_chego=""):
    matrica = [[1, 0, 0, dx], [0, 1, 0, dy], [0, 0, 1, dz], [0, 0, 0, 1]]

    if dly_chego == "for_normal":
        matrica = np.linalg.inv(matrica)
        matrica = np.transpose(matrica)

    multiplication(matrica, lst_v)
    return


# Отражение
def reflection(os_xyz, lst_v, dly_chego=""):
    if os_xyz == "x":
        matrica = [[-1, 0, 0, 0], [0, 1, 0, 0], [0, 0, 1, 0], [0, 0, 0, 1]]
    elif os_xyz == "y":
        matrica = [[1, 0, 0, 0], [0, -1, 0, 0], [0, 0, 1, 0], [0, 0, 0, 1]]
    elif os_xyz == "z":
        matrica = [[1, 0, 0, 0], [0, 1, 0, 0], [0, 0, -1, 0], [0, 0, 0, 1]]
    elif os_xyz == "xyz":
        # Отразить по всем сразу
        matrica = [[-1, 0, 0, 0], [0, -1, 0, 0], [0, 0, -1, 0], [0, 0, 0, 1]]

    if dly_chego == "for_normal":
        matrica = np.linalg.inv(matrica)
        matrica = np.transpose(matrica)

    multiplication(matrica, lst_v)
    return

# Умножение матриц
def multiplication(matrica, lst_v):
    for i in range(len(lst_v[0])):
        vector = [lst_v[0][i], lst_v[1][i], lst_v[2][i], lst_v[3][i]]
        res = np.dot(matrica, vector)
        lst_v[0][i] = res[0]
        lst_v[1][i] = res[1]
        lst_v[2][i] = res[2]
        lst_v[3][i] = res[3]
    return

# Переход в СК камеры
def multiplication_M2_on(lst_v, pic_size, dly_chego=""):
    l = - pic_size / 2
    r = pic_size / 2
    b = - pic_size / 2
    t = pic_size / 2
    n = 200
    f = 700

    matrica_M2 = [[2*n / (l-r), 0, (r + l) / (r - l), 0],
                [0, 2*n / (t - b), (t + b) / (t - b), 0],
                [0, 0, (f + n) / (f - n), 2 * f * n / (n-f)],
                [0, 0, 1, 0]]
    if dly_chego == "for_normal":
        matrica_M2 = np.linalg.inv(matrica_M2)
        matrica_M2 = np.transpose(matrica_M2)

    multiplication(matrica_M2, lst_v)

    return

def projection(lst_v, pic_size):
    for i in range(len(lst_v[0])):
        lst_v[0][i] *= pic_size / 2
        lst_v[0][i] += pic_size / 2
        lst_v[0][i] = int(lst_v[0][i] - 0.5)
        # int ( _ + 0.5) => чтобы округлить
        # int ( _ + 0.5) - 1 == int ( _ - 0.5)
        # Поэтому не так lst_v[0][i] = int(lst_v[0][i] + 0.5) - 1

        lst_v[1][i] *= pic_size / 2
        lst_v[1][i] += pic_size / 2
        lst_v[1][i] = int(lst_v[1][i] - 0.5)

        lst_v[2][i] *= pic_size / 2
        lst_v[2][i] += pic_size / 2
        lst_v[2][i] = int(lst_v[2][i] - 0.5)

        lst_v[3][i] *= pic_size / 2
        lst_v[3][i] += pic_size / 2
        lst_v[3][i] = int(lst_v[3][i] - 0.5)

    return

# Деление на 4ую координату
def delenie_on_4_coord(lst_v):
    for i in range(len(lst_v[0])):
        if not lst_v[3][i] == 0:
            lst_v[0][i] /= lst_v[3][i]
            lst_v[1][i] /= lst_v[3][i]
            lst_v[2][i] /= lst_v[3][i]
            lst_v[3][i] /= lst_v[3][i]
    return