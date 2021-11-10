from hashlib import sha256
import random
import threading
from threading import Thread
import time
import itertools
from concurrent.futures.process import ProcessPoolExecutor

charset = "abcdefghijklmnopqrstuvwxyz"

def thread1(m):
    password = ""
    for i in range(5):
        password += random.choice(charset)
    hash = sha256(password.encode('utf-8')).hexdigest()
    while (m != hash):
        password = ""
        for i in range(5):
            password += random.choice(charset)
        hash = sha256(password.encode('utf-8')).hexdigest()
        #print(password)

    print(m, " = ", password)

print("Введите количество потоков")
l = int(input())
print("Введите хэщ-значение")
n = input()
start_time = time.time()
for i in range(l):
    th = Thread(target=thread1, args=(n, ))
    th.start()
th.join()
print("--- %s seconds ---" % (time.time() - start_time))