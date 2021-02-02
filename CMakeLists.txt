cmake_minimum_required(VERSION 3.17)
project(DataChannelUnity)

#set(CMAKE_FIND_DEBUG_MODE 1)
add_compile_options(/MP)

add_subdirectory(${PROJECT_SOURCE_DIR}/libdatachannel)
set(LIBDATACHANNEL_INCLUDE_DIR ${PROJECT_SOURCE_DIR}/libdatachannel/include)

find_package(OpenSSL REQUIRED)

add_subdirectory(src)
