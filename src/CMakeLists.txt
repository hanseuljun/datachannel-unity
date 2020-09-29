add_library(DataChannelUnity SHARED
  plugin.cpp
)
target_include_directories(DataChannelUnity PRIVATE
  ${LIBDATACHANNEL_INCLUDE_DIR}
)
target_link_libraries(DataChannelUnity PRIVATE
  datachannel-static
)
set_target_properties(DataChannelUnity PROPERTIES
  CXX_STANDARD 17
)
