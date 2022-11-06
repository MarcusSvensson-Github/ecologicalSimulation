import matplotlib.pyplot as plt


class Plotter():
    """
        En klass för att skapa en plot från filerna

        ...

        Attributes:
        -----------
        file : str
            Name of the file to be opened
        xlabel : str
            Label for the x axis
        ylabel : str
            Label for the y axis

        Methods:
        --------
        plot()
            Create the plot and show it

    """

    def __init__(self, file: str, xlabel: str, ylabel: str):
        self.file = file
        self.xlabel = xlabel
        self.ylabel = ylabel
        self.data = self.readFile()
        self.xData = []
        self.yData = []
        self.axisData()

    def readFile(self):
        with open(self.file) as f:
            return f.readlines()

    def axisData(self):
        self.data = self.data[1::]
        for line in self.data:
            line = line.replace(',', '.')
            line = line.rstrip('\n')
            line = line.split()
            self.xData.append(line[1])
            self.yData.append(line[0])

        self.fixData()

    def fixData(self):
        self.xData = [int(float(i)) for i in self.xData]
        self.yData = [float(i) for i in self.yData]

    def plot(self):
        plt.plot(self.xData[::10], self.yData[::10])

        plt.ylabel(self.ylabel)
        plt.xlabel(self.xlabel)

        plt.show()


if __name__ == '__main__':
    t = Plotter('bytePop.txt', 'xlabel', 'ylabel')
    t.plot()
