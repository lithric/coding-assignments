from math import floor
iteration = 0
while (iteration < 1):
    try:
        check_number = int(input("Enter a number: "))
        print(check_number)
    except ValueError:
        print("you're not a nice person")
        break
    check_number = abs(check_number)
    check_range = floor(pow(check_number, 0.5))
    div_number = 2
    while(div_number <= check_range):
        if (check_number % div_number == 0):
            print(str(div_number)+" x "+str(floor(check_number/div_number))+" not prime")
            break
        div_number += 1
    if (div_number > check_range):
        print("prime")
        break
    iteration +=1