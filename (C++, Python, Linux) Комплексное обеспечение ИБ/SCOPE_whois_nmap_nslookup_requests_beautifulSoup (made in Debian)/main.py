from ipwhois import IPWhois
import whois
import nmap
from parsing import get_ccTLD
from parsing import get_subdomains_parse
from other_func import host_to_ip
from other_func import get_subdomains_nmap

print("Start!")
ranges = set()# Здесь будут диапазоны IP
first = []# Деления IP
second = []

# СЕРВЕРЫ
print("Servers...")
main_who_is = whois.query('google.com')
nm = nmap.PortScanner()
f = open('Servers.txt', 'w')
# По всем серверам
for server in main_who_is.name_servers:
    # Вытащить IP сервера
    nm.scan(server, '80', arguments='-T5')
    f.write("Server name: " + server + "\n")
    all_hosts = nm.all_hosts()
    if all_hosts and len(all_hosts) > 0:
        f.write("IP-address: " + all_hosts[0] + "\n")
    # По IP серверов (для гугла каждый сервер имеет свой IP)
    for ip in nm.all_hosts():
        obj = IPWhois(ip)
        results = obj.lookup_rdap()

        f.write("Address:" + results['objects']['ZG39-ARIN']['contact']['address'][0]['value'].replace("\n", ", ") +
                "\n")
        f.write("Phone:" + results['objects']['ZG39-ARIN']['contact']['phone'][0]['value'] + "\n")
        f.write("Email:" + results['objects']['ZG39-ARIN']['contact']['email'][0]['value'] + "\n\n")
        # Не записывать второй раз, если этот диапазон уже есть
        values = ip.split('.')
        if not (values[0] in first and values[1] in second):
            ranges.add(results['network']['start_address'] + "-" + results['network']['end_address'])
f.close()

# TLD
print("TLD...")
all_ccTLD = get_ccTLD()
print("TLD -> IP...")
f = open('TLD.txt', 'w')
for tld in all_ccTLD:
    ip_tld = host_to_ip(tld)
    if ip_tld:
        f.write(ip_tld + "\t" + tld + "\n")

        values = ip_tld.split('.')
        # Не искать второй раз, если этот диапазон уже есть
        if not (values[0] in first and values[1] in second):
            first.append(values[0])
            second.append(values[1])
            results = IPWhois(ip_tld).lookup_rdap()
            ranges.add(results['network']['start_address'] + "-" + results['network']['end_address'])
    else:
        f.write("\t" + tld + "\n")
        print("Для " + tld + " не удалось найти IP")

f.close()

# SUBDOMAINS
print("Subdomains...")
all_subD = set()
# Через nmap
""" Если все TLD проверить.
for dom in all_ccTLD:
    # Добавляем, если их еще нет
    all_subD = all_subD.union(get_subdomains_nmap(dom))"""
all_subD = all_subD.union(get_subdomains_nmap("google.com"))
all_subD = all_subD.union(get_subdomains_nmap("google.ru"))

# Через парсинг сайта
all_subD = all_subD.union(get_subdomains_parse())

print("Subdomains -> IP...")
f = open("Subdomains.txt", 'w')
for subD in all_subD:
    ip_subD = host_to_ip(subD)
    if ip_subD:
        f.write(ip_subD + "\t" + subD + "\n")
        values = ip_subD.split('.')
        # Не искать второй раз, если этот диапазон уже есть
        if not (values[0] in first and values[1] in second):
            first.append(values[0])
            second.append(values[1])
            results = IPWhois(ip_subD).lookup_rdap()
            ranges.add(results['network']['start_address'] + "-" + results['network']['end_address'])
    else:
        f.write("\t" + subD + "\n")
        print("Для " + subD + " не удалось найти IP")
f.close()

print("Ranges IP...")
f = open('Ranges IP.txt', 'w')
for ran in ranges:
    f.write(ran + "\n")
f.close()

print("Finish!")
