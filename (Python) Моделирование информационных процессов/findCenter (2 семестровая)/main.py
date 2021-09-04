import matplotlib.pyplot as plt
import numpy as np

n=10
r=20
tay=[2,1,3,1,4,5,2,2,1,4]

dec_round = 4
circle = plt.Circle((0, 0), r, color='black', fill=False)
ax = plt.subplots()[1].add_artist(circle)
plt.ylim(ymin=-21, ymax=21)
plt.xlim(xmin=-28, xmax=28)

x = np.zeros(n)
y = np.zeros(n)

for point in range(n):
	rad = np.random.uniform(0, 2*np.pi)
	x[point] = np.round(r*np.cos(rad), dec_round)
	y[point] = np.round(r*np.sin(rad), dec_round)
	plt.plot(x[point], y[point], marker='o', color='r')
	plt.annotate(str(point+1) + " t=" + str(tay[point]), xy=(x[point], y[point]+0.5), size=12, weight='bold')
	#plt.annotate(str(point+1), xy=(x[point]+0.5, y[point]+1), size=12)
	print("Точка" + str(point+1) + ": \t\tx=" + str(x[point]) + "\t\ty=" + str(y[point]) + "\t\tt=" + str(tay[point]))

sum_chisl_x = sum_chisl_y = sum_znam = 0
for i, t in enumerate(tay):
	sum_chisl_x += t * x[i]
	sum_chisl_y += t * y[i]
	sum_znam += t
	
center_x = np.round(sum_chisl_x/sum_znam, dec_round)
center_y = np.round(sum_chisl_y/sum_znam, dec_round)

plt.plot(center_x, center_y, marker='o', color='r')
print("Центр:\tx=" + str(center_x) + "\ty=" + str(center_y))
print("Расстояние до (0,0) =", np.round(np.sqrt(np.power(center_x, 2) + np.power(center_y, 2)),dec_round))
plt.show()
