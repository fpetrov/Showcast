import datetime

import grpc
from concurrent import futures

import greet_pb2
import greet_pb2_grpc


class GreeterServices(greet_pb2_grpc.GreeterServicer):
    def SayHello(self, request, context):
        print(request)

        hello_reply = greet_pb2.HelloReply()
        hello_reply.message = str(datetime.datetime.now().time())

        return hello_reply


def serve():
    print('Starting server...')
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))

    greet_pb2_grpc.add_GreeterServicer_to_server(GreeterServices(), server)
    server.add_insecure_port('[::]:5001')
    server.start()
    server.wait_for_termination()


if __name__ == '__main__':
    serve()
