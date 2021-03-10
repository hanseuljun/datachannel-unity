cmake_minimum_required(VERSION 3.16)
project(DataChannelUnity)

#set(CMAKE_FIND_DEBUG_MODE 1)
add_compile_options(/MP)

find_package(OpenSSL REQUIRED)

add_subdirectory(${PROJECT_SOURCE_DIR}/libdatachannel)
set(LIBDATACHANNEL_INCLUDE_DIR ${PROJECT_SOURCE_DIR}/libdatachannel/include)

add_subdirectory(src)
