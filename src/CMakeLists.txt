add_library(DataChannelUnity SHARED
  capi_plugin.cpp
  capi_plugin_ex.cpp
  IUnityInterface.h
)
target_include_directories(DataChannelUnity PRIVATE
  ${LIBDATACHANNEL_INCLUDE_DIR}
)
target_link_libraries(DataChannelUnity PRIVATE
  datachannel
)
set_target_properties(DataChannelUnity PROPERTIES
  CXX_STANDARD 17
)
