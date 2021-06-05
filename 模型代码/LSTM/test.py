import torch
import numpy as np
from model import lstm
from torch.utils.data import Dataset
from process_data import pro_data
import matplotlib.pyplot as plt
from torch.autograd import Variable
from model import backNum,hidden


#超参数
backNum=8


if __name__ == '__main__':
    alldata = pro_data()

    net = lstm(backNum, hidden)
    net.load_state_dict(torch.load('./weights/123.pth'))
    net.eval()
    # data_X = data_X.reshape(-1, 1, backNum)
    # data_X = torch.from_numpy(data_X)
    var_data = Variable(alldata.test_x)
    pred_test = net(var_data)
    pred_test = pred_test.view(-1).data.numpy()
    plt.plot(pred_test, 'r', label='prediction')
    plt.plot(alldata.test_y, 'b', label='real')
    plt.legend(loc='best')
    plt.show()


    




