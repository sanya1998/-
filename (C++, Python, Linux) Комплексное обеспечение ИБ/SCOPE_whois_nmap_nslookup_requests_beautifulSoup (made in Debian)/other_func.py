import nmap
import socket
from pprint import pprint
from bs4 import BeautifulSoup


def host_to_ip(host):
    try:
        # 1 попытка извлечь IP через сокет
        return socket.gethostbyname(host)
    except:
        try:
            # 2 попытка извлечь IP через nmap
            nm = nmap.PortScanner()
            nm.scan(host, '80', arguments='-T5')
            return nm.all_hosts()[0]
        except:
            return None
    return None


def get_subdomains_nmap(domain):
    nm = nmap.PortScanner()
    nm.scan(domain, '80', arguments='-T5 --script dns-brute')
    subd = set()
    soup = BeautifulSoup(nm.get_nmap_last_output(), 'lxml')
    for stroka in soup.script['output'].split('\n'):
        if stroka.find(domain) > 0:
            subd.add(stroka.split(" - ")[0].replace(" ", ""))

    return subd


"""testing"""
"""from ipwhois import IPWhois
from pprint import pprint
import dns.resolver

nm = nmap.PortScanner()
#asdfg = "216.239.32-63.*"
asdfg = "216.239.32-33.*"
nm.scan(asdfg, '80')
print(asdfg, nm.all_hosts())
print(asdfg, nm.get_nmap_last_output())
print(asdfg, nm.analyse_nmap_xml_scan())
pprint(IPWhois('172.217.21.140').lookup_rdap())

print(socket.gethostbyaddr('172.217.21.142'))
print(socket.gethostbyname('google.com'))"""
