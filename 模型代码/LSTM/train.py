import matplotlib.pyplot as plt
import torch
from torch import nn
from torch.utils import data
from torch.autograd import Variable
from torch.utils.data import DataLoader
import torchvision.datasets as dsets
import torchvision.transforms as transforms
from model import lstm
from process_data import pro_data
from model import backNum,hidden






alldata = pro_data()

net = lstm(backNum, hidden)
criterion = nn.MSELoss()
optimizer = torch.optim.Adam(net.parameters(), lr=1e-2)

for e in range(200):
    net.train()
    var_x = Variable(alldata.train_x, requires_grad=True)
    var_y = Variable(alldata.train_y, requires_grad=True)
    # 前向传播
    out = net(var_x)
    loss = criterion(out, var_y)
    # 反向传播
    optimizer.zero_grad()
    loss.backward()
    optimizer.step()
    if (e + 1) % 10 == 0: # 每 10 次输出结果
        net.eval()
        output=net(var_x)
        loss = criterion(out, var_y)
        print(loss)
out = out.view(-1).data.numpy()
var_y = var_y.view(-1).data.numpy()
plt.plot(out, 'r', label='prediction')
plt.plot(var_y, 'b', label='real')
plt.legend(loc='best')
plt.show()
torch.save(net.state_dict(), './weights/123.pth')