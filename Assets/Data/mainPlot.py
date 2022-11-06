import plot as p


if __name__ == '__main__':
    popByte = p.Plotter('bytePop.txt', 'Tid(s)', 'Population Bytesdjur')
    popByte.plot()

    popRov = p.Plotter('rovPop.txt', 'Tid(s)', 'Population Rovdjur')
    popRov.plot()

    speedByte = p.Plotter('byteSpeed.txt', 'Tid(s)', 'Hastighet Bytesdjur')
    speedByte.plot()

    speedRov = p.Plotter('rovSpeed.txt', 'Tid(s)', 'Hastighet Rovdjur')
    speedRov.plot()

    visionByte = p.Plotter('byteVision.txt', 'Tid(s)', 'Synradie Bytesdjur')
    visionByte.plot()

    visionRov = p.Plotter('rovVision.txt', 'Tid(s)', 'Synradie Rovdjur')
    visionRov.plot()
