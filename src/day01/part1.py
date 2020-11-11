with open('input.txt', 'r') as f:
    start = 0
    for line in f:
        sign = line[0]
        number = int(line[1:])
        if sign == '-':
            start -= number
        elif sign == '+':
            start += number

    print(start)
