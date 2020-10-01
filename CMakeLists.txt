cmake_minimum_required(VERSION 3.17)
project(DataChannelUnity)

#set(CMAKE_FIND_DEBUG_MODE 1)

add_subdirectory(${PROJECT_SOURCE_DIR}/libdatachannel EXCLUDE_FROM_ALL)
set(LIBDATACHANNEL_INCLUDE_DIR ${PROJECT_SOURCE_DIR}/libdatachannel/include)

find_package(OpenSSL REQUIRED)

add_subdirectory(src)
