import numpy as np

# deg(многочлена)
m = 4
# длина кода
n = 2 ** m - 1
# Диапазон количества информационных бит
k_min = 1
# n - deg(многочлена)
k_max = 7

# Поле GF(2^4) при помощи неприводимого примитивного многочлена h(x)=1+x+x^4, степени alpha
gf_2_4 = [[1, 0, 0, 0], [0, 1, 0, 0], [0, 0, 1, 0], [0, 0, 0, 1],
          [1, 1, 0, 0], [0, 1, 1, 0], [0, 0, 1, 1], [1, 1, 0, 1],
          [1, 0, 1, 0], [0, 1, 0, 1], [1, 1, 1, 0], [0, 1, 1, 1],
          [1, 1, 1, 1], [1, 0, 1, 1], [1, 0, 0, 1]]

print("Длина исходного сообщения от", k_min, "до", k_max,".")
x_str = input("Введите сообщение из 0 и 1:")
# x_str = "1001011"

k = len(x_str)
if k_min > k or k > k_max:
	print("Ошибка! Неправильная длина сообщения!")
	exit()

x_send = []
for ch in x_str:
	if ch == '1':
		x_send.append(1)
	elif ch == '0':
		x_send.append(0)
	else:
		print("Ошибка! Недопустимые символы!")
		exit()
x_send = np.array(x_send, dtype=int)
print("Исходное сообщение:\t\t\t\t", x_send)

# Возьмем корни alpha^1 и alpha^3
# Минимальные многочлены соответсвенно x^4+x+1 и x^4+x^3+x^2+x+1
# g(x) = m1(x)*m2(x) = 1+x^4+x^6+x^7+x^8
g_x = [1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0]

# Порождающая матрица
G = []
for i in range(k):
	# np.roll - циклический сдвиг
	G.append(np.roll(g_x, i))
G = np.array(G)

y_send = (x_send @ G) % 2
print("Отправлено кодовое сообщение:\t", y_send)
# Программно делаются ошибки
y_get = y_send
# 0,1 или 2 ошибки
for i in range(np.random.randint(0, 3)):
	pos = np.random.randint(0, len(y_get))
	if y_get[pos] == 1:
		y_get[pos] = 0
	elif y_get[pos] == 0:
		y_get[pos] = 1
print("Получено кодовое сообщение:\t\t", y_get)

# Проверочная матрица
H = []
for i in range(n):
	# alfa^1
	a_1 = gf_2_4[(i * 1) % n]
	# alfa^3
	a_3 = gf_2_4[(i * 3) % n]
	# Стобец (пока в виде строки)
	stolbec_H = np.array(a_1 + a_3)
	H.append(stolbec_H.transpose())
H = np.array(H)

syndrom = H.T @ y_get % 2
y_correct = np.array(y_get)
print("Синдром:", (syndrom))

s_1 = syndrom[:4]
s_3 = syndrom[4:]

print("S1 =", s_1)
print("S3 =", s_3)
# 1 и 4 случай
if np.array_equal(s_1, [0,0,0,0]):
	# 1 случай
	if np.array_equal(s_3, [0,0,0,0]):
		print("Нет ошибок")
	# 4 случай
	else:
		print("Произошло более 2 ошибок! Исправить нельзя")
# 2 и 3 случай
else:
	ind_s_1 = gf_2_4.index(list(s_1))
	ind_s_1_in3 = (ind_s_1 * 3) % len(gf_2_4)
	s_1_in3 = gf_2_4[ind_s_1_in3]
	# 2 случай
	if np.array_equal(s_1_in3, s_3):
		pos = gf_2_4.index(list(s_1))
		print("Произошла 1 ошибка на позиции:", pos)
		if y_get[pos] == 1:
			y_correct[pos] = 0
		elif y_get[pos] == 0:
			y_correct[pos] = 1
	# 3 случай
	# elif not np.array_equal(s_3, [0,0,0,0]):
	else:
		print("Произошла двойная ошибка")
		ind_s_1_in2 = (ind_s_1 * 2) % len(gf_2_4)
		if np.array_equal(s_3, [0,0,0,0]):
			free_part = np.array(gf_2_4[ind_s_1_in2])
		else:
			ind_s_3 = gf_2_4.index(list(s_3))
			ind_s_3_del_s_1 = ind_s_3 - ind_s_1
			if ind_s_3_del_s_1 < 0:
				ind_s_3_del_s_1 += len(gf_2_4)
			free_part = np.array(gf_2_4[ind_s_1_in2]) + np.array(gf_2_4[ind_s_3_del_s_1])
		for pos in range(len(gf_2_4)):
			step = (pos * 2) % len(gf_2_4)
			x_in2 = np.array(gf_2_4[step])
			ind_s_1_na_x = (ind_s_1 + pos) % len(gf_2_4)
			
			left = (x_in2 + np.array(gf_2_4[ind_s_1_na_x]) + free_part) % 2
			if np.array_equal(left, [0,0,0,0]):
				print("Ошибка на позиции:", pos)
				if y_get[pos] == 1:
					y_correct[pos] = 0
				elif y_get[pos] == 0:
					y_correct[pos] = 1

print("Исправленное кодовое сообщение:\t", y_correct)

# Декодирование кодового сообщения в исходное
# Деление кодового многочлена на порождающий
# y_correct / g_x
y_correct = y_correct[::-1]
while y_correct[0] == 0:
	y_correct = np.delete(y_correct, 0)
g_x = g_x[::-1]
while g_x[0] == 0:
	g_x = np.delete(g_x, 0)
x_get = np.array((np.polydiv(y_correct, g_x)[0][::-1]+2)%2,dtype=int)
while len(x_get) < k:
	x_get = np.append(x_get, 0)
	
print("Декодированное сообщение:\t\t", x_get)
