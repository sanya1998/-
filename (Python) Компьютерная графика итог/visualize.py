import numpy as np
import math

def draw_faces(lst_v, lst_vt, lst_vn, lst_f, pic_size, image, L, V, img_np_texture):

    img_z = np.zeros((pic_size, pic_size))
    img_z.fill(min(lst_v[2])-1)

    length_L = np.linalg.norm(L)
    length_V = np.linalg.norm(V)

    for i in range(len(lst_f[0][0])):

        # 2coord - 1 coord
        a_x = lst_v[0][lst_f[1][0][i]] - lst_v[0][lst_f[0][0][i]]
        a_y = lst_v[1][lst_f[1][0][i]] - lst_v[1][lst_f[0][0][i]]
        a_z = lst_v[2][lst_f[1][0][i]] - lst_v[2][lst_f[0][0][i]]
        # 3coord - 1coord
        b_x = lst_v[0][lst_f[2][0][i]] - lst_v[0][lst_f[0][0][i]]
        b_y = lst_v[1][lst_f[2][0][i]] - lst_v[1][lst_f[0][0][i]]
        b_z = lst_v[2][lst_f[2][0][i]] - lst_v[2][lst_f[0][0][i]]
        # np.cross - Векторное произведение = Нормаль
        N = np.cross([a_x, a_y, a_z], [b_x, b_y, b_z])
        length_N = np.linalg.norm(N)
        
        if not length_L == 0 and not length_N == 0:
            # Этот косинус общий для всего трегольника (можно убрать, когда получится каждый пиксель расскрасить)
            # np.dot(L, N) - Скалярное произведение
            c_o_s_LN = np.dot(L, N) / (length_L * length_N)

            paint_triangle (lst_v[0][lst_f[0][0][i]], lst_v[0][lst_f[1][0][i]], lst_v[0][lst_f[2][0][i]],
                            lst_v[1][lst_f[0][0][i]], lst_v[1][lst_f[1][0][i]], lst_v[1][lst_f[2][0][i]],
                            lst_v[2][lst_f[0][0][i]], lst_v[2][lst_f[1][0][i]], lst_v[2][lst_f[2][0][i]],
                            lst_v[3][lst_f[0][0][i]], lst_v[3][lst_f[1][0][i]], lst_v[3][lst_f[2][0][i]],
                            lst_vt[0][lst_f[0][1][i]], lst_vt[0][lst_f[1][1][i]], lst_vt[0][lst_f[2][1][i]],
                            lst_vt[1][lst_f[0][1][i]], lst_vt[1][lst_f[1][1][i]], lst_vt[1][lst_f[2][1][i]],
                            lst_vn[0][lst_f[0][2][i]], lst_vn[0][lst_f[1][2][i]], lst_vn[0][lst_f[1][2][i]],
                            lst_vn[1][lst_f[0][2][i]], lst_vn[1][lst_f[1][2][i]], lst_vn[1][lst_f[2][2][i]],
                            lst_vn[2][lst_f[0][2][i]], lst_vn[2][lst_f[1][2][i]], lst_vn[2][lst_f[2][2][i]],
                            image, img_z, img_np_texture, pic_size, L, V, length_L, length_V, c_o_s_LN)

    return np.rot90(image, 1)



def paint_triangle(x1, x2, x3,
                   y1, y2, y3,
                   z1, z2, z3,
                   w1, w2, w3,
                   u1, u2, u3,
                   v1, v2, v3,
                   n_x_1, n_x_2, n_x_3,
                   n_y_1, n_y_2, n_y_3,
                   n_z_1, n_z_2, n_z_3,
                   image, img_z, img_np_texture, pic_size,
                   L, V,
                   length_L, length_V,
                    cos_obshi):

    min_x = min(x1, x2, x3)
    max_x = max(x1, x2, x3)
    min_y = min(y1, y2, y3)
    max_y = max(y1, y2, y3)

    x0 = min_x
    while x0 <= max_x:
        y0 = min_y
        while y0 <= max_y:
            # Сам вывел с помощью лекции
            zn = (y2 - y3) * (x1 - x3) - (x2 - x3) * (y1 - y3)
            if zn == 0:
                # чтобы не рассматривать эту точку
                a = 1
                b = -1
            else:
                a = ((y2 - y3) * (x0 - x3) - (x2 - x3) * (y0 - y3)) / zn
                b = ((y0 - y3) * (x1 - x3) - (x0 - x3) * (y1 - y3)) / zn
            c = 1 - a - b

            if a >= 0 and b >= 0 and c >= 0 or a <= 0 and b <= 0 and c <= 0:
                a = np.abs(a)
                b = np.abs(b)
                c = np.abs(c)
                # Нормаль для точки внутри треугольника
                Normal = ([(a*n_x_1 + b*n_x_2 + c*n_x_3), (a*n_y_1 + b*n_y_2 + c*n_y_3), (a*n_z_1 + b*n_z_2 + c*n_z_3)])
                length_Normal = np.linalg.norm(Normal)

                # np.dot(L, N) - Скалярное произведение
                c_o_s_LN = np.dot(L, Normal) / (length_L * length_Normal)
                c_o_s_LN = cos_obshi

                if c_o_s_LN < 0:
                    """if z1 == 0 or z2 == 0 or z3 == 0 or (a == 0 and b == 0 and c == 0):
                        # Что-то по умнее надо написать)
                        # z_current = 3 * z1 * z2 * z3 / (z1 * z2 + z2 * z3 + z3 * z1)
                        z_current = max(z1, z2, z3)
                        u = v = 0
                    else:"""
                    z_current = 1 / (a / z1 + b / z2 + c / z3)

                    if img_z[x0, y0] < z_current:

                        u = a * u1 / z1 + b * u2 / z2 + c * u3 / z3
                        v = a * v1 / z1 + b * v2 / z2 + c * v3 / z3

                        col_1 = img_np_texture[int(u * pic_size + 0.5), int(v * pic_size + 0.5)][0] * (-c_o_s_LN)
                        col_2 = img_np_texture[int(u * pic_size + 0.5), int(v * pic_size + 0.5)][1] * (-c_o_s_LN)
                        col_3 = img_np_texture[int(u * pic_size + 0.5), int(v * pic_size + 0.5)][2] * (-c_o_s_LN)

                        # Модель Фонга
                        # Находим отраженный луч
                        koef = 2 *(np.dot(L, Normal)/np.dot(Normal, Normal))
                        L_volna = ([koef*Normal[0] - L[0], koef*Normal[1] - L[1], koef*Normal[2] - L[2]])

                        length_L_volna = np.linalg.norm(L_volna)

                        c_o_s_V_Lvolna = np.dot(V, L_volna) / (length_V * length_L_volna)
                        alfa = 10
                        blick = 255 * math.pow(c_o_s_V_Lvolna, alfa)

                        if col_1 + blick > 255:
                            col_1 = 255
                        else:
                            col_1 += blick

                        if col_2 + blick > 255:
                            col_2 = 255
                        else:
                            col_2 += blick

                        if col_3 + blick > 255:
                            col_3 = 255
                        else:
                            col_3 += blick

                        color = (col_1, col_2, col_3)

                        image[x0, y0] = color
                        img_z[x0, y0] = z_current
            y0 += 1
        x0 += 1
    return
