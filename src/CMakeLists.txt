add_library(DataChannelUnityPlugin SHARED
  plugin.cpp
)
target_include_directories(DataChannelUnityPlugin PRIVATE
  ${LIBDATACHANNEL_INCLUDE_DIR}
)
target_link_libraries(DataChannelUnityPlugin PRIVATE
  datachannel-static
)
set_target_properties(DataChannelUnityPlugin PROPERTIES
  CXX_STANDARD 17
)
