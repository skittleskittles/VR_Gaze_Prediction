from torch import nn
from torch.autograd import Variable


#超参数
backNum=5   #回看多少个
hidden = 12


class lstm(nn.Module):
    def __init__(self, input_size, hidden_size, output_size=1, num_layers=3):
        super(lstm, self).__init__()
        
        self.rnn = nn.LSTM(input_size, hidden_size, num_layers) # rnn
        self.reg = nn.Linear(hidden_size, output_size) # 回归
        
    def forward(self, x):
        x, _ = self.rnn(x) # (seq, batch, hidden)
        s, b, h = x.shape
        x = x.view(s*b, h) # 转换成线性层的输入格式
        x = self.reg(x)
        x = x.view(s, b, -1)
        return x