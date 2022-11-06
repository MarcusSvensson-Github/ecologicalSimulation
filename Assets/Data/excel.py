from asyncore import write
import xlsxwriter


def readFile(filename: str):
    with open(filename) as f:
        lines = f.readlines()

        xdata = []
        ydata = []

        lines = lines[1::]

        for line in lines:
            line = line.replace(',', '.')
            line = line.rstrip('\n')
            line = line.split()

            xdata.append(line[1])
            ydata.append(line[0])

        return(xdata, ydata)


def writeToWorksheet(xdata: list, ydata: list, worksheet):
    ycol = 0
    xcol = 1
    row = 0
    for x, y in zip(xdata, ydata):
        worksheet.write_number(row, xcol, int(float(str(x))))

        worksheet.write_number(row, ycol, float(str(y)))
        row += 1


if __name__ == '__main__':

    workbook = xlsxwriter.Workbook('data.xlsx')
    w1 = workbook.add_worksheet('bytesPop')
    w2 = workbook.add_worksheet('rovPop')
    w3 = workbook.add_worksheet('byteSpeed')
    w4 = workbook.add_worksheet('rovSpeed')
    w5 = workbook.add_worksheet('byteVision')
    w6 = workbook.add_worksheet('rovVision')

    xdata, ydata = readFile('bytePop.txt')
    writeToWorksheet(xdata, ydata, w1)
    xdata = []
    ydata = []

    #xdata, ydata = readFile('rovPop.txt')
    #writeToWorksheet(xdata, ydata, w2)

    xdata, ydata = readFile('byteSpeed.txt')
    writeToWorksheet(xdata, ydata, w3)

    #xdata, ydata = readFile('rovSpeed.txt')
    #writeToWorksheet(xdata, ydata, w2)

    xdata, ydata = readFile('byteVision.txt')
    writeToWorksheet(xdata, ydata, w2)

    #xdata, ydata = readFile('rovVision.txt')
    #writeToWorksheet(xdata, ydata, w2)

    workbook.close()
