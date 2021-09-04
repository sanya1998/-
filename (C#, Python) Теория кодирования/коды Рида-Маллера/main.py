# Коды Рида-Маллера порядка 1. (r = 1, m - произвольное)
import numpy as np

m = int(input("Введите m = "))
if m < 3:
	print("Ошибка! m должна быть целым и больше 2")
	exit()
correct_mistake = 2 ** (m - 2) - 1
print("Код исправляет: " + str(correct_mistake) + " ошибки(ок) в кодовых сообщениях")

print("Длина исходного сообщения:", m + 1)

x_str = input("Введите сообщение из 0 и 1:")
if len(x_str) != m + 1:
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

G_0_m = np.ones(shape=(1, 2 ** m))
G_1_m = []
# Формирование G(1,m)
for i in range(m):
	line = list()
	while len(line) < 2 ** m:
		for j in range(2 ** (m - i - 1)):
			line.append(0)
		for j in range(2 ** (m - i - 1)):
			line.append(1)
	G_1_m.append(line)
G = np.array(np.concatenate((G_0_m, np.array(G_1_m))), dtype=int)

y_send = (x_send @ G) % 2
print("Отправлено кодовое сообщение:\t", y_send)

# Программно делаются ошибки
y_get = y_send
for i in range(np.random.randint(1, correct_mistake+1)):
	pos = np.random.randint(0, len(y_get))
	if y_get[pos] == 1:
		y_get[pos] = 0
	elif y_get[pos] == 0:
		y_get[pos] = 1
print("Получено кодовое сообщение:\t\t", y_get)

# Генерация матрицы Адамара
H = np.array([[1, 1], [1, -1]])
for i in range(1, m):
	H = np.concatenate((np.concatenate((H, H)), np.concatenate((H, -H))), 1)

Y_get = 2*y_get - 1
multiplication_Y_H = Y_get @ H
i_max_abs = np.argmax(np.abs(multiplication_Y_H))
if multiplication_Y_H[i_max_abs] > 0:
	y_correct = np.array((1 + H[i_max_abs]) / 2, dtype=int)
else:
	y_correct = np.array((1 - H[i_max_abs]) / 2, dtype=int)
print("Исправленное кодовое сообщение:\t", y_correct)

# Декодирование кодового сообщения
x_get = [y_correct[0]]
for i in range(1, m+1):
	x_get.append((x_get[0] + y_correct[2**(m-i)]) % 2)
x_get = np.array(x_get)
print("Декодированное сообщение:\t\t", x_get)
