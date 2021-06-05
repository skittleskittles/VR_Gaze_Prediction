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
from torch.utils.data import DataLoader
import torch.utils.data as Data



BATCH_SIZE = 64


alldata = pro_data()

net = lstm(backNum, hidden)
criterion = nn.MSELoss()
optimizer = torch.optim.Adam(net.parameters(), lr=1e-2)
torch_dataset = Data.TensorDataset(alldata.train_x,alldata.train_y)
loader = Data.DataLoader(dataset=torch_dataset,batch_size=BATCH_SIZE,shuffle=True,num_workers=2)

for e in range(200):
    net.train()
    for iteration, (batch_x, batch_y) in enumerate(loader):
        # 前向传播
        out = net(batch_x)
        loss = criterion(out, batch_y)
        optimizer.zero_grad()
        loss.backward()
        optimizer.step()
        # 反向传播
        optimizer.zero_grad()
        loss.backward()
        optimizer.step()
    if (e + 1) % 10 == 0: # 每 10 次输出结果
        net.eval()
        output=net(alldata.train_x)
        loss = criterion(out, alldata.train_y)
        print(loss)
out = out.view(-1).data.numpy()
var_y = alldata.train_y.view(-1).data.numpy()
plt.plot(out, 'r', label='prediction')
plt.plot(var_y, 'b', label='real')
plt.legend(loc='best')
plt.show()
torch.save(net.state_dict(), './weights/123.pth')