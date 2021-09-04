def parsing_file(path_to_file, lst_v, lst_vt, lst_vn, lst_f):
    f = open(path_to_file)
    for line in f:
        array_from_str = line.split()
        if line.startswith('v '):
            for i in range(1, len(array_from_str)):
                # np.append(lst_v[i-1], float(array_from_str[i]))
                lst_v[i-1].append(float(array_from_str[i]))
            lst_v[3].append(1)
        elif line.startswith('vt '):
            for i in range(1, len(array_from_str)):
                lst_vt[i-1].append(float(array_from_str[i]))
        elif line.startswith('vn '):
            for i in range(1, len(array_from_str)):
                lst_vn[i-1].append(float(array_from_str[i]))
            lst_vn[3].append(1)
        elif line.startswith('f '):
            # lst_f[j][q][i] : j - номер тройки в строчке, q - номер числа в тройке, i - номер строчки в файле,
            # пусть из i-ой строчки: f 1/2/3 4/5/6 7/8/9
            # нам надо взять число 6: lst_f[1][2][i]
            for i in range(1, len(array_from_str)):
                troyka_chisel_f = array_from_str[i].split("/")
                for j in range(len(troyka_chisel_f)):
                    lst_f[i-1][j].append(int(troyka_chisel_f[j])-1)
    f.close()
