import random

batch_size = 1000

rangeStart = int(input("Enter start range: "))
rangeEnd = int(input("Enter end range: "))

rangeLength = abs(rangeStart - rangeEnd)
randNumb = random.randint(rangeStart,rangeEnd)

batch_rangeStart = rangeStart
batch_rangeEnd = rangeStart + min(rangeLength, batch_size)
batch_total = int(rangeLength/batch_size)
batch_number = 0

guess_list = range(batch_rangeStart,batch_rangeEnd)
i = 0
while (i < rangeLength):
    guess = guess_list[i - batch_size*batch_number]+batch_size*batch_number
    if (guess == randNumb):
        print("the number is: "+str(guess))
        print("in batch: "+str(batch_number))
        break
    elif ((i+1) % batch_size == 0):
        print("batch "+str(batch_number)+" out of "+str(batch_total)+": "+str(int(batch_number*100/batch_total))+"%")
        batch_number += 1
    i += 1