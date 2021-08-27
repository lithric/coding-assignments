from math import floor
iteration = 0
while (iteration < 1):
    try:
        check_number = int(input("Enter a number: "))
        check_number = 2**31 - 1
        print(check_number)
    except ValueError:
        print("you're not a nice person")
        break
    check_number = abs(check_number)
    check_range = floor(pow(check_number, 0.5))
    div_number = 2
    div_list = []
    while(div_number <= check_range):
        cond = True
        while (cond):
            cond = False
            for item in div_list:
                if (div_number % item == 0):
                    div_number += 1
                    cond = True
                    break
        if (check_number % div_number == 0):
            print(str(div_number)+" x "+str(floor(check_number/div_number))+" not prime")
            break
        div_list.append(div_number)
        div_number += 1
    if (div_number > check_range):
        print("prime")
        break
    iteration +=1