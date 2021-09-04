import numpy as np
from openpyxl import Workbook

n=5
b=7
k=9 # Порядковый номер

# Округлять до ..
decimals_round = 3
print("k =", k)
print("f_i(x) = (1,5 + sin (i) ) * ln (1 + k*x)")
matrix_f = np.zeros(shape=(n,b+1))
for i in range(n):
	for x in range(b+1):
		matrix_f[i][x] = np.round((1.5+np.sin(i+1))*np.log1p(k*(x)),decimals_round)
# print(matrix_f)
print("a(i,x) = f_i(x) - f_i(x-1)")
# Формирование матрицы разностей
matrix_a = np.zeros(shape=(n,b))
for i in range(n):
	for j in range(b):
		matrix_a[i][j] = matrix_f[i][j+1]-matrix_f[i][j]
# print(matrix_a)

list_pos_in_str=np.zeros(n, dtype=int)
f=0
print("f="+str(f))
for iter in range(b):
	step_in_str=0
	max_val = np.min(np.array(matrix_a))
	for nom_str, pos in enumerate(list_pos_in_str):
		if matrix_a[nom_str][pos] > max_val:
			max_val = matrix_a[nom_str][pos]
			step_in_str = nom_str
	f += matrix_a[step_in_str][list_pos_in_str[step_in_str]]
	print(str(iter+1) + ") k = " + str(step_in_str+1), "\tx" + str(step_in_str+1) + " = " + str(list_pos_in_str[step_in_str]+1),
	      "\tf = f + " + str(np.round(matrix_a[step_in_str][list_pos_in_str[step_in_str]],decimals_round)) + " = " + str(np.round(f,decimals_round)))
	
	list_pos_in_str[step_in_str] += 1
print("x*=" + str(list_pos_in_str))
print("f=" + str(np.round(f,decimals_round)))

# Запись в excel
wb = Workbook()
ws = wb.active
# Заносим таблицу маленьких f
ws.append(["x"] + list(np.arange(b+1)))
for i, line in enumerate(matrix_f):
	ws.append(["f"+str(i+1) + "(x)"] + list(line))
ws.append([])
ws.append(["x"] + list(np.arange(b)+1))
for i, line in enumerate(matrix_a):
	ws.append(["a("+str(i+1) + ",x)"] + list(line))
wb.save('sample.xlsx')