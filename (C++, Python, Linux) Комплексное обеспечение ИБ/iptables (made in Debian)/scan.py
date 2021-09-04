import socket


def scan(net_beg_f, net_end_f, ports_f):
    n_b = [int(item) for item in net_beg_f.split('.')]
    n_e = next_ip([int(item) for item in net_end_f.split('.')])
    while n_b != n_e:
        current_ip = str(n_b[0]) + '.' + str(n_b[1]) + '.' + str(n_b[2]) + '.' + str(n_b[3])
        for port in ports_f:
            print("IP: " + current_ip, "\tPort: " + str(port), "\t" + check_port(current_ip, port))

        n_b = next_ip(n_b)

    return None


def next_ip(net_list4):
    # Переход к следующему IP
    net_list4[3] += 1
    if net_list4[3] == 256:
        net_list4[3] = 0
        net_list4[2] += 1
        if net_list4[2] == 256:
            net_list4[2] = 0
            net_list4[1] += 1
            if net_list4[1] == 256:
                net_list4[1] = 0
                net_list4[0] += 1
                if net_list4[0] == 256:
                    net_list4[0] = 0
    return net_list4


def check_port(ip, port, time_out=0.1):
    try:
        socket.setdefaulttimeout(time_out)  # seconds (float)
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)  # TCP
        # sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # UDP
        result = sock.connect_ex((ip, port))
        if result == 0:
            # print ("Port is open")
            # final[ip] = "OPEN"
            answer = "OPEN"
        else:
            # print ("Port is closed/filtered")
            # final[ip] = "CLOSED"
            answer = "CLOSED"
        sock.close()
    except:
        pass
    return answer


# Порты для анализа
ports = [22, 43, 53, 80, 443, 631]

# Получить различные диапазоны
nets = []
f = open("Ranges IP.txt", 'r')
for line in f:
    line = line.replace('\n', "")
    nets.append(line.split('-'))
f.close()

"""
# Проверить все диапазоны IP (долго)
for net in nets:
    net_beg = net[0]
    net_end = net[1]
    scan(net_beg, net_end, ports)
"""

# Чисто пример
scan("216.239.35.250", "216.239.36.20", ports)

# Проверить свои порты
net_beg = "127.0.0.1"
net_end = "127.0.0.1"
scan(net_beg, net_end, ports)

# Открывается, если активный (не открывается, если никакое приложение его не ждет)
# Закрыть iptables -A INPUT -p tcp -m tcp --dport 631 -j DROP
# Открыть iptables -D INPUT -p tcp -m tcp --dport 631 -j DROP
#
