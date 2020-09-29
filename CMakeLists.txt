cmake_minimum_required(VERSION 3.17)
project(DataChannelUnity)

add_subdirectory(${PROJECT_SOURCE_DIR}/libdatachannel EXCLUDE_FROM_ALL)
set(LIBDATACHANNEL_INCLUDE_DIR ${PROJECT_SOURCE_DIR}/libdatachannel/include)

add_subdirectory(src)
