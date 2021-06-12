import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import torch
from torch.utils import data
from model import backNum,hidden





class pro_data():

    def create_dataset(self,look_back=backNum):
        dataX, dataY = [], []
        for i in range(len(self.dataset) - look_back):
            a = self.dataset[i:(i + look_back)]
            dataX.append(a)
            dataY.append(self.dataset[i + look_back])
        return np.array(dataX), np.array(dataY)

    def __init__(self):
        df = pd.read_csv('./CameraData.csv', usecols=[1])
        #plt.plot(df)
        #plt.show()
        data_csv = df.dropna()
        dataset = data_csv.values
        dataset = dataset.astype('float32')
        scalar = 360
        self.dataset = list(map(lambda x: x / scalar, dataset))
        data_X, data_Y = self.create_dataset()
        train_size = int(len(data_X) * 0.7)
        test_size = len(data_X) - train_size
        # train_X = data_X[:train_size]
        # train_Y = data_Y[:train_size]
        # test_X = data_X[train_size:]
        # test_Y = data_Y[train_size:]
        train_X = data_X
        train_Y = data_Y
        test_X = data_X
        test_Y = data_Y
        

        train_X = train_X.reshape(-1, 1, backNum)
        train_Y = train_Y.reshape(-1, 1, 1)
        test_X = test_X.reshape(-1, 1, backNum)
        test_Y = test_Y.reshape(-1, 1, 1)
        self.train_x = torch.from_numpy(train_X)
        self.train_y = torch.from_numpy(train_Y)
        self.test_x = torch.from_numpy(test_X)
        self.test_y = torch.from_numpy(test_Y)


class pro_data_e():

    def create_dataset(self,look_back=backNum):
        dataX, dataY = [], []
        for i in range(len(self.dataset) - look_back):
            a = self.dataset[i:(i + look_back)]
            dataX.append(a)
            dataY.append(self.dataset[i + look_back])
        return np.array(dataX), np.array(dataY)

    def __init__(self):
        df = pd.read_csv('./CameraData.csv', usecols=[0])
        #plt.plot(df)
        #plt.show()
        data_csv = df.dropna()
        dataset = data_csv.values
        dataset = dataset.astype('float32')
        for i in range(dataset.size):
            if dataset[i]<=90:
                dataset[i]+=90
            else:
                dataset[i]-=270
        scalar = 360
        self.dataset = list(map(lambda x: x / scalar, dataset))
        data_X, data_Y = self.create_dataset()
        train_size = int(len(data_X) * 0.7)
        test_size = len(data_X) - train_size
        # train_X = data_X[:train_size]
        # train_Y = data_Y[:train_size]
        # test_X = data_X[train_size:]
        # test_Y = data_Y[train_size:]
        train_X = data_X
        train_Y = data_Y
        test_X = data_X
        test_Y = data_Y
        

        train_X = train_X.reshape(-1, 1, backNum)
        train_Y = train_Y.reshape(-1, 1, 1)
        test_X = test_X.reshape(-1, 1, backNum)
        test_Y = test_Y.reshape(-1, 1, 1)
        self.train_x = torch.from_numpy(train_X)
        self.train_y = torch.from_numpy(train_Y)
        self.test_x = torch.from_numpy(test_X)
        self.test_y = torch.from_numpy(test_Y)

        




# if __name__ == '__main__':
#     pro_data()



