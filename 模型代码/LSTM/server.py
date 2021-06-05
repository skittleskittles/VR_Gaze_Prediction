from socket import *
from numpy.lib import NumpyVersion
import torch
import numpy as np
from model import lstm
from torch.utils.data import Dataset
from process_data import pro_data
import matplotlib.pyplot as plt
from torch.autograd import Variable
from model import backNum,hidden
import sys

    

def main():
    net = lstm(backNum, hidden)
    net.load_state_dict(torch.load('./weights/123.pth'))
    net.eval()
    total = np.zeros(8)
    
    # 1.创建套接字socket
    tcp_socket = socket(AF_INET, SOCK_STREAM)
    # 2.绑定信息bind
    tcp_socket.bind(("127.0.0.1", 5000))
    # 3.设置为监听状态listen
    tcp_socket.listen(128)

    # 循环多次accept,为多个客户端服务
    while True:
        print("Wait Client...")

        # 等待客户端连接accept
        new_client, client_addr = tcp_socket.accept()
        # 打印接入客户端地址信息
        print("Access Client:%s : %d" % (client_addr[0], client_addr[1]))

        # 为同一个客户端多次服务
        while True:
            # 接收客户端发送来的请求
            new_data = new_client.recv(1024)
            ndata = new_data.decode("gbk")
            input = ndata.split()
            nums=[float(num) for num in input]
            input = np.array(nums)
            input = input.astype('float32')
            output1 = input[1]+(input[1]-input[0])
            output2 = input[4]+(input[4]-input[3])*2
            output = str(output1)+" "+str(output2)


            # np.append(total,input)
            # tmp_total = torch.from_numpy(total)
            # tmp_total=tmp_total.reshape(-1, 1, backNum)
            # var_data = Variable(tmp_total)
            # var_data= torch.tensor(var_data, dtype=torch.float32)
            # pred_test = net(var_data)
            # num = float(pred_test[0][0][0])
            # num = num*360
            # num = round(num,4)
            # output = str(num)

            # recv解堵塞两种方式：1.客户端发来数据  2.客户端调用close
            if new_data:
                # 打印客户端发送到来的信息
                print("message:%s" % new_data.decode("gbk"))
                # 反馈客户端信息接收到
                new_client.send(str.encode(output))
            else: 
                break

        # 关闭该客户端套接字,不再为此客户端服务
        new_client.close()
        print("Done...")

    # 关闭套接字
    tcp_socket.close()


if __name__ == '__main__':

    main()