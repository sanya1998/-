answer = '-1'
while answer != '0':
	print("\n0 - выход")
	print("1 - заменить символ")
	print("2 - не хватает символа")
	print("3 - лишний символ")
	answer = input("Ваш выбор: ")
	if answer == '0':
		break
	elif answer == '1' or answer == '2' or answer == '3':
		word = input("Слово из 0 и 1: ")
		
		s_u = 0
		for i, ch in enumerate(word, start=1):
			if ch != '0' and ch != '1':
				print('Ошибка! Есть недопустимые символы!')
				exit()
			s_u += i * int(ch)
			
		# Произошла замена символа
		if answer == '1':
			l = len(word) + 1
			n = len(word)
			s_u_mod_l = s_u % l
			
			print("s(u) =", s_u, "\ts(u) mod", l, "=", s_u_mod_l)
			
			if s_u_mod_l == 0:
				print("В слове нет ошибки!")
				continue
				
			ind_err = s_u_mod_l - 1
			if word[ind_err] == '1':
				word = word[:ind_err] + '0' + word[ind_err + 1:]
			elif word[ind_err] == '0':
				word = word[:ind_err] + '1' + word[ind_err + 1:]
		
		# Произошло выпадение символа
		elif answer == '2':
			l = len(word) + 2
			n = len(word) + 1
			s_u_mod_l = s_u % l
			
			print("s(u) =", s_u, "\ts(u) mod", l, "=", s_u_mod_l)

			minus_s_u = - s_u
			minus_s_u_mod_l = minus_s_u
			while minus_s_u_mod_l < 0:
				minus_s_u_mod_l += l
			w_u = 0
			for ch in word:
				if ch == '1':
					w_u += 1
			print("w(u) =", w_u, "\t-s(u) mod", l, "=", minus_s_u_mod_l)
			if minus_s_u_mod_l > w_u:
				# Выпала '1'
				count_0_right = 0
				# n-2, потому что выпал 1 символ
				ind=-1
				for i in range(n-2, -1, -1):
					if count_0_right == n - minus_s_u_mod_l:
						ind = i
						break
					if word[i] == '0':
						count_0_right += 1
				word = word[:ind+1] + '1' + word[ind+1:]
			else:
				# Выпал '0'
				count_1_right = 0
				# n-2, потому что выпал 1 символ
				ind=-1
				for i in range(n-2, -1, -1):
					if count_1_right == minus_s_u_mod_l:
						ind = i
						break
					if word[i] == '1':
						count_1_right += 1
				word = word[:ind+1] + '0' + word[ind+1:]
		
		# 	Произошло добавление символа
		elif answer == '3':
			l = len(word)
			n = len(word) - 1
			k = s_u_mod_l = s_u % l
			
			print("s(u) =", s_u, "\tk = s(u) mod", l, "=", s_u_mod_l)
			w_u = 0
			for ch in word:
				if ch == '1':
					w_u += 1
			print("w(u) =", w_u)
			
			if k == 0:
				word = word[:len(word)-1]
			elif 0 < k < w_u:
				count_1_right = 0
				# n, потому что 1 лишний символ
				ind = 0
				for i in range(n, -1, -1):
					if count_1_right == k:
						ind = i
						break
					if word[i] == '1':
						count_1_right += 1
				# По условию надо отбросить именно '0'
				if word[ind] == '0':
					word = word[:ind] + word[ind + 1:]
				else:
					print("Ошибка! Не найден ноль, правее которого", k, "единиц!")
			elif k == w_u:
				word = word[1:]
			elif k > w_u:
				count_0_right = 0
				# n, потому что 1 лишний символ
				ind = 0
				for i in range(n, -1, -1):
					if count_0_right == n+1-k:
						ind = i
						break
					if word[i] == '0':
						count_0_right += 1
				# По условию надо отбросить именно '1'
				if word[ind] == '1':
					word = word[:ind] + word[ind + 1:]
				else:
					print("Ошибка! Не найдена единица, правее которой", n+1-k, "нулей!")
		
		print("Исправленное слово:", word)
	
