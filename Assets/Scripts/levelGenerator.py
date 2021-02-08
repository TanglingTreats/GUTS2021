from random import randint
import csv

with open('level.csv', mode="w", newline='') as level:
    level_writer = csv.writer(level, delimiter=',',
                              quotechar='"', quoting=csv.QUOTE_MINIMAL)
    for x in range(100):
        level_writer.writerow(
            [randint(10, 95), randint(10, 95), randint(0, 1), randint(-100, 100), randint(-100, 100)])
