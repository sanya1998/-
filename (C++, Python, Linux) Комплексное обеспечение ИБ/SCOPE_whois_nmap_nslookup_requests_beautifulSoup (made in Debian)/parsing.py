from bs4 import BeautifulSoup
import requests as req


def get_subdomains_parse():
    resp = req.get("https://ru.wikipedia.org/wiki/Список_служб_и_проектов_Google")
    soup = BeautifulSoup(resp.text, 'lxml')
    subdomains = set()
    for a in soup.find_all("a"):
        if a.has_attr("href"):
            if a["href"].find("google.") != -1:
                # Ссылка на продукт гугла, убрать начало
                link = a['href'].replace("http://www.", "").replace("https://www.", "").replace("http://", "").replace("https://", "")
                # Убрать '/' и все, что после него
                pos = link.find('/')
                if pos != -1:
                    link = link[:pos]
                # Заодно убедиться, что он все еще входит в имя
                if link.find("google.") > 0:
                    # Если это поддомен google, то добавить
                    subdomains.add(link)
    return list(subdomains)


def get_ccTLD():
    resp = req.get("https://ru.wikipedia.org/wiki/Список_доменов_Google")
    soup = BeautifulSoup(resp.text, 'lxml')

    ccTLD = set()
    for a in soup.find_all("a"):
        if a.has_attr("href"):
            if a["href"].find("google.") != -1:
                # Ссылка на гугл, убрать начало
                link = a['href']\
                    .replace("http://www.", "")\
                    .replace("https://www.", "")\
                    .replace("http://", "")\
                    .replace("https://", "")
                # Убрать '/' и все, что после него
                pos = link.find('/')
                if pos != -1:
                    link = link[:pos]
                # Если гугл в начале (искл: igoogle)
                if link.find("google") == 0 or link.find("google") == 1:
                    ccTLD.add(link)
    return list(ccTLD)