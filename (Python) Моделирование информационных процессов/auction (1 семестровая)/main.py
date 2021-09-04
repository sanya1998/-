import numpy as np
import matplotlib.pyplot as plt
import matplotlib.ticker as ticker

fig, ax = plt.subplots()

k = 23
n = 10
b = 30

c = np.zeros(n)
d = np.zeros(n)

for i in range(n):
	c[i] = np.round(3 + 2*np.sin(k * (i+1)), 3)
	d[i] = np.random.randint(2, 10)
	print("c(" + str(i+1) + ") = " + str(c[i]) + "\t d(" + str(i+1) + ") = " + str(d[i]))

indexes_sort = np.argsort(c)

# Build graphic
array_x = np.array([])
array_y = np.array([])
last_y = 0

min_price = max(c)
profit = 0
print("Profit = ")
for ind in indexes_sort[:: -1]:
	if last_y < b:
		profit += c[ind] * min(d[ind], b-last_y)
		print("c(" + str(ind+1) + ") * " + str(min(d[ind], b-last_y)))
		min_price = c[ind]
	
	array_x = np.append(array_x, c[ind])
	array_y = np.append(array_y, last_y)
	last_y += d[ind]
	array_x = np.append(array_x, c[ind])
	array_y = np.append(array_y, last_y)
	#plt.axvline(x=c[ind], color='gray', linestyle=':', linewidth=1)
	#plt.annotate("i=" + str(ind+1), xy=(c[ind], last_y), color='black')
	plt.annotate(str(ind+1), xy=(c[ind], last_y), color='black', size=10)

array_x = np.append(array_x, 0)
array_y = np.append(array_y, last_y)

print("= " + str(np.round(profit, 4)))
print("Цена отсечения = " + str(min_price))

plt.plot(array_x, array_y)
plt.ylim(ymin=0)
plt.xlim(xmin=0)

#  Устанавливаем интервал основных и
#  вспомогательных делений:
ax.xaxis.set_major_locator(ticker.MultipleLocator(1))
ax.xaxis.set_minor_locator(ticker.MultipleLocator(0.2))
ax.yaxis.set_major_locator(ticker.MultipleLocator(10))
ax.yaxis.set_minor_locator(ticker.MultipleLocator(2))

#  Включаем видимость вспомогательных делений:
"""ax.minorticks_on()
ax.grid(which='major',
        color = 'k',
        linestyle = ':' )

ax.grid(which='minor',
        color = 'gray',
        linestyle = ':')"""

plt.axhline(y=b, color='r', linestyle='-')

plt.show()

