import matplotlib.pyplot as plt
import torch
from torch import nn
from torch.utils import data
from torch.autograd import Variable
from torch.utils.data import DataLoader
import torchvision.datasets as dsets
import torchvision.transforms as transforms
from model import lstm
from process_data import pro_data, pro_data_e
from model import backNum,hidden
from torch.utils.data import DataLoader
import torch.utils.data as Data




alldata = pro_data()
alldata_e = pro_data_e()



net = lstm(backNum, hidden)
#net.load_state_dict(torch.load('./weights/123.pth'))
criterion = nn.MSELoss()
optimizer = torch.optim.Adam(net.parameters(), lr=1e-2)

for e in range(200):
    net.train()
    var_x = Variable(alldata.train_x)
    var_y = Variable(alldata.train_y)
    # 前向传播
    out = net(var_x)
    loss = criterion(out, var_y)
    # 反向传播
    optimizer.zero_grad()
    loss.backward()
    optimizer.step()
    if (e + 1) % 10 == 0: # 每 10 次输出结果
        net.eval()
        output=net(alldata.train_x)
        loss = criterion(output, alldata.train_y)
        print(loss)
net.eval()
out=net(alldata.train_x)
out = out.view(-1).data.numpy()
var_y = alldata.train_y.view(-1).data.numpy()
plt.plot(out, 'r', label='prediction')
plt.plot(var_y, 'b', label='real')
plt.legend(loc='best')
plt.show()
torch.save(net.state_dict(), './weights/123.pth')



#net.load_state_dict(torch.load('./weights/123e.pth'))

for e in range(200):
    net.train()
    var_x = Variable(alldata_e.train_x)
    var_y = Variable(alldata_e.train_y)
    # 前向传播
    out = net(var_x)
    loss = criterion(out, var_y)
    # 反向传播
    optimizer.zero_grad()
    loss.backward()
    optimizer.step()
    if (e + 1) % 10 == 0: # 每 10 次输出结果
        net.eval()
        output=net(alldata_e.train_x)
        loss = criterion(output, alldata_e.train_y)
        print(loss)
net.eval()
out=net(alldata_e.train_x)
out = out.view(-1).data.numpy()
var_y = alldata_e.train_y.view(-1).data.numpy()
plt.plot(out, 'r', label='prediction')
plt.plot(var_y, 'b', label='real')
plt.legend(loc='best')
plt.show()
torch.save(net.state_dict(), './weights/123e.pth')



