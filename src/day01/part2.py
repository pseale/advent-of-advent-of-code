with open('input.txt') as f:
    lines = f.readlines()

count = 0
for line in lines:
    count += int(line)

print("Part 1: " + str(count))

counts = set()
count = 0
ind = 0
while (True):
    count += int(lines[ind % len(lines)])
    if (count in counts):
        break
    counts.add(count)
    ind += 1

print("Part 2: " + str(count))