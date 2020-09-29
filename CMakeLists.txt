cmake_minimum_required(VERSION 3.17)
project(DataChannelUnity)

add_subdirectory(${PROJECT_SOURCE_DIR}/libdatachannel EXCLUDE_FROM_ALL)
set(LIBDATACHANNEL_INCLUDE_DIR ${PROJECT_SOURCE_DIR}/libdatachannel/include)

#set(CMAKE_FIND_DEBUG_MODE 1)
find_package(OpenSSL REQUIRED)
message("OpenSSL: ${OpenSSL_FOUND}")

add_subdirectory(src)
