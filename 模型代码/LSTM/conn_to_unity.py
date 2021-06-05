import torch
import numpy as np
from model import lstm
from torch.utils.data import Dataset
from process_data import pro_data
import matplotlib.pyplot as plt
from torch.autograd import Variable
from model import backNum,hidden
import sys


def pred(a, b, c, d, e):
    net = lstm(backNum, hidden)
    net.load_state_dict(torch.load('./weights/123.pth'))
    net.eval()
    test = np.array([a, b, c, d, e])
    test = test.astype('float32')
    test = torch.from_numpy(test)
    test=test.reshape(-1, 1, backNum)
    var_data = Variable(test)
    pred_test = net(var_data)
    return float(pred_test[0][0][0])





if __name__ == "__main__":
    print(pred(sys.argv[1],sys.argv[2],sys.argv[3],sys.argv[4],sys.argv[5]))
