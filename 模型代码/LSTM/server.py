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

    nete = lstm(backNum, hidden)
    nete.load_state_dict(torch.load('./weights/123e.pth'))
    nete.eval()
    
    # 1.创建套接字socket
    tcp_socket = socket(AF_INET, SOCK_STREAM)
    # 2.绑定信息bind
    tcp_socket.bind(("127.0.0.1", 5000))
    # 3.设置为监听状态listen
    tcp_socket.listen(128)

    mydb = np.array([[0,0,0,0,0]])
    mydb = mydb.astype('float32')
    mydbe = np.array([[0,0,0,0,0]])
    mydbe = mydbe.astype('float32')

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
            print(input)
            input = input.astype('float32')
            for i in range(5):
                if input[i+5]<=90:
                    input[i+5]+=90
                else:
                    input[i+5]-=270
            for i in range(input.size):
                input[i]=input[i]/360
            input=input.reshape(1,-1)



            # np.append(total,input)
            tmp = input[0][0:5]
            tmp = tmp.reshape(1,-1)
            mydb = np.r_[mydb,tmp]
            tmp_mydb = torch.from_numpy(mydb)
            tmp_mydb=tmp_mydb.reshape(-1, 1, backNum)
            var_data = Variable(tmp_mydb)
            var_data= torch.tensor(var_data, dtype=torch.float32)
            pred_test = net(var_data)
            num = float(pred_test[mydb.shape[0]-1][0][0])
            num = num*360
            num = round(num,4)
            output1 = str(num)

            tmp = input[0][5:10]
            tmp = tmp.reshape(1,-1)
            mydbe = np.r_[mydbe,tmp]
            tmp_mydb = torch.from_numpy(mydbe)
            tmp_mydb=tmp_mydb.reshape(-1, 1, backNum)
            var_data = Variable(tmp_mydb)
            var_data= torch.tensor(var_data, dtype=torch.float32)
            pred_test = nete(var_data)
            num = float(pred_test[mydbe.shape[0]-1][0][0])
            num = num*360
            if num<=90:
                num+=270
            else:
                num-=90
            num = round(num,4)
            output2 = str(num)

            # recv解堵塞两种方式：1.客户端发来数据  2.客户端调用close
            if new_data:
                # 打印客户端发送到来的信息
                #print("message:%s" % new_data.decode("gbk"))
                # 反馈客户端信息接收到
                output = output1 + " " + output2
                print(output)
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