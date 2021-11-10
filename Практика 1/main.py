import win32api
import win32file
import shutil
import os


def is_drive_ready(drive_name):
    try:
        win32api.GetVolumeInformation(drive_name)
        return True
    except:
        return False


# Задание 1
drives = win32api.GetLogicalDriveStrings()
drives = drives.split('\000')[:-1]
print(drives)
for letter in drives:
    print("Название: ", letter)
    print("Тип: ", win32file.GetDriveType(letter))
    # 0 	Unknown
    # 1 	No Root Directory
    # 2 	Removable Disk
    # 3 	Local Disk
    # 4 	Network Drive
    # 5 	Compact Disc
    # 6 	RAM Disk
    if is_drive_ready(letter):
        total, used, free = shutil.disk_usage(letter)
        print("Всего: %d ГБ" % (total // (2 ** 30)))
        print("Использовано: %d ГБ" % (used // (2 ** 30)))
        print("Свободно: %d ГБ" % (free // (2 ** 30)))
        t = win32api.GetVolumeInformation(letter)
        print("Метка:", t[0])
    print("------------------------------")

# Задание 2

my_file = open("Test.txt", "w+")
my_file.write("Hello world")
my_file.close()
my_file = open("Test.txt", "r")
print(my_file.read())
my_file.close()
os.remove(my_file.name)

# Задание 3
import json

data = {'age': 100, 'name': 'mkyong.com', 'messages': ['msg 1', 'msg 2', 'msg 3']}
outfile = open('data.json', 'w+')
json.dump(data, outfile)
outfile.close()
outfile = open('data.json', 'r+')
print(outfile.read())
outfile.close()
os.remove(outfile.name)

# Задание 4

import xml.etree.ElementTree as ET

p = ET.Element('parent')
c = ET.SubElement(p, 'child1')
tree = ET.ElementTree(p)
tree.write("sample.xml")

tree = ET.parse('sample.xml')
root = tree.getroot()
element = root[0]
ET.SubElement(element, 'child2')
tree.write("sample.xml")

ET.dump(tree)

os.remove('sample.xml')

# Задание 5
import zipfile

newzip = zipfile.ZipFile('zzz.zip', 'w',zipfile.ZIP_DEFLATED)
newzip.write('forzip.txt')
newzip.close()

newzip = zipfile.ZipFile('zzz.zip', 'r',zipfile.ZIP_DEFLATED)

newzip.extractall()
newzip.printdir()
newzip.close()
os.remove('zzz.zip')
