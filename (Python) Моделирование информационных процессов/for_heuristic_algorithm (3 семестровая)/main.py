import matplotlib.pyplot as plt
import numpy as np
from openpyxl import Workbook
import matplotlib.ticker as ticker


def general_chart_parameters(plt, n, r, x, y):
	fig, ax = plt.subplots()
	
	#  Устанавливаем интервал основных
	ax.xaxis.set_major_locator(ticker.MultipleLocator(5))
	ax.yaxis.set_major_locator(ticker.MultipleLocator(5))
	ax.grid(which='major', color='black', linestyle='-')
	
	# #  и вспомогательных делений:
	# ax.xaxis.set_minor_locator(ticker.MultipleLocator(1))
	# ax.yaxis.set_minor_locator(ticker.MultipleLocator(1))
	# ax.grid(which='minor', color='gray', linestyle='-')
	
	#  Включаем видимость вспомогательных делений:
	ax.minorticks_on()
	
	plt.ylim(ymin=-21, ymax=21)
	plt.xlim(xmin=-28, xmax=28)
	
	# Окружность
	circle = plt.Circle((0, 0), r, color='black', fill=False, linestyle="-")
	ax.add_artist(circle)
	
	for point in range(n):
		plt.plot(x[point], y[point], marker='H', color='navy', markersize=7)
		# plt.annotate("i=" + str(point), xy=(x[point], y[point]), size=12)
		plt.annotate(" " + str(point), xy=(x[point], y[point]), size=10)


# Округлять до столько знаков после запятой:
decimals_round = 2
n = 11
r = 20

x = np.zeros(n)
y = np.zeros(n)
a = np.zeros(n)

x[0] = y[0] = a[0] = 0
# это в excel
# Запись в excel
wb = Workbook()
ws = wb.active
ws.append(["x(0)=" + str(x[0])] + ["y(0)=" + str(y[0])] + ["a(0)=" + str(a[0])])
# print("x(0)=" + str(x[0]) + "\t\ty(0)=" + str(y[0]) + "\t\ta(0)=" + str(a[0]))
# Случайно расставить точки внутри круга
for point in range(1, n):
	# В полярных координатах
	angle = np.random.uniform(0, 2*np.pi)
	radius = np.random.uniform(2, r)
	# В декартовые координаты
	x[point] = np.round(radius*np.cos(angle),decimals_round)
	y[point] = np.round(radius*np.sin(angle),decimals_round)
	
	a[point] = np.round(3*np.abs(np.sin(point)), decimals_round)
	# Это надо сразу в excel
	ws.append(["x(" + str(point) + ")=" + str(x[point])] + ["y(" + str(point) + ")=" + str(y[point])] + ["a(" + str(point) + ")=" + str(a[point])])
	# print("x(" + str(point) + ")=" + str(x[point]) + "\t\ty(" + str(point) +")=" + str(y[point]) + "\t\ta("+str(point)+")=" + str(a[point]))
print("Таблица вершин записана excel")
	
# Matrix весов
distance = np.zeros(shape=(n, n))
for i in range(len(distance)):
	for j in range(len(distance[0])):
		distance[i][j] = np.round(np.sqrt((x[j] - x[i])**2 + (y[j] - y[i])**2),decimals_round)

ws.append([])
ws.append(["vertices"] + list(range(11)))
for i, subarray in enumerate(distance):
	ws.append([i] + list(subarray))
wb.save('sample.xlsx')
print("Таблица весов записана excel")

# Алгоритм Краскала без ограничений
general_chart_parameters(plt, n, r, x, y)
# Те, которые уже есть в графе
graf = np.zeros(n)
graf[0] = 1

ves_graf = 0
print("Без ограничений")
nomer=0
while 0 in graf:
	i_mem=j_mem=0
	min_dist = np.max(distance) + 1
	for i in range(n):
		# Если первая вершина в графе
		if graf[i] == 1:
			# Найти минимальное расстояние
			for j in range(n):
				# Если второй вершины нет в графе
				if graf[j] == 0:
					# Здесь будет условие на выполнение ограничения
					# Запоминаем min_dist
					if distance[i][j] < min_dist:
						min_dist = distance[i][j]
						i_mem = i
						j_mem = j
	# Проводим линию i_mem-j_mem
	plt.plot([x[i_mem], x[j_mem]], [y[i_mem], y[j_mem]])
	# Помечаем, что вершина уже есть в графе
	graf[j_mem] = 1
	# К сумме добавляем длину новой дуги
	ves_graf += distance[i_mem][j_mem]
	nomer += 1
	print(str(nomer) + ") Проводим дугу:", i_mem, "и", j_mem)
print("Вес без ограничений: " + str(np.round(ves_graf, decimals_round)))
plt.show()


# Алгоритм Краскала без ограничений
d = 7

general_chart_parameters(plt, n, r, x, y)
# Те, которые уже есть в графе
graf = np.zeros(n)
graf[0] = 1
ogranich_esli_y_korny = np.zeros(n)

ves_graf = 0
pathes_by_root = list()
for i in range(n):
	pathes_by_root.append([])
pathes_by_root[0].append(0)
print("Ограничение: d = ", d)
nomer=0
while 0 in graf:
	i_mem=j_mem=0
	min_dist = np.max(distance) + 1
	for i in range(n):
		# Если первая вершина в графе
		if graf[i] == 1:
			# Найти минимальное расстояние
			for j in range(n):
				# Если второй вершины нет в графе
				if graf[j] == 0:
					new_ogr = a[j]
					if i != 0:
						# номер предпоследней
						n_pp = len(pathes_by_root[i]) - 2
						# сама предпоследняя
						sama_pp = pathes_by_root[i][n_pp]
						# К предпоследней добавляем ограничение
						new_ogr += ogranich_esli_y_korny[sama_pp]
					# Условие на выполнение ограничения
					if new_ogr <= d:
						# Запоминаем min_dist
						if distance[i][j] < min_dist:
							min_dist = distance[i][j]
							i_mem = i
							j_mem = j
	# Проводим линию i_mem-j_mem
	plt.plot([x[i_mem], x[j_mem]], [y[i_mem], y[j_mem]])
	# Помечаем, что вершина уже есть в графе
	graf[j_mem] = 1
	# К сумме добавляем длину новой дуги
	ves_graf += distance[i_mem][j_mem]
	
	nomer += 1
	print(str(nomer) + ") Проводим дугу:", i_mem, "и", j_mem)
	# Записываем для новой вершины путь до корня
	pathes_by_root[j_mem] = [j_mem] + pathes_by_root[i_mem]
	# номер предпоследней
	n_pp = len(pathes_by_root[j_mem]) - 2
	# сама предпоследняя
	sama_pp = pathes_by_root[j_mem][n_pp]
	# К предпоследней добавляем ограничение
	ogranich_esli_y_korny[sama_pp] += a[j_mem]

print("Вес с ограничением: " + str(np.round(ves_graf, decimals_round)))
plt.show()
