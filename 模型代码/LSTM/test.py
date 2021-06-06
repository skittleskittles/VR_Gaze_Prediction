from conn_to_unity import pred
import torch
import numpy as np
from model import lstm
from torch.utils.data import Dataset
from process_data import pro_data,pro_data_e
import matplotlib.pyplot as plt
from torch.autograd import Variable
from model import backNum,hidden




if __name__ == '__main__':
    alldata = pro_data()
    alldata_e = pro_data_e()

    net = lstm(backNum, hidden)
    net.load_state_dict(torch.load('./weights/123.pth'))
    net.eval()
    var_data = Variable(alldata.test_x)
    pred_test = net(var_data)
    pred_test = pred_test.view(-1).data.numpy()
    real_test = alldata.test_y.view(-1).data.numpy()
    plt.plot(pred_test, 'r', label='prediction')
    plt.plot(real_test, 'b', label='real')
    plt.legend(loc='best')
    plt.show()

    net = lstm(backNum, hidden)
    net.load_state_dict(torch.load('./weights/123e.pth'))
    net.eval()
    var_data = Variable(alldata_e.test_x)
    pred_test = net(var_data)
    pred_test = pred_test.view(-1).data.numpy()
    real_test = alldata_e.test_y.view(-1).data.numpy()
    plt.plot(pred_test, 'r', label='prediction')
    plt.plot(real_test, 'b', label='real')
    plt.legend(loc='best')
    plt.show()


    




