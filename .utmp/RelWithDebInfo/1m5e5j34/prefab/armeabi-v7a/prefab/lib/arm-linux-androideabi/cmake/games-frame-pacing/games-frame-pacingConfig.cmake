if(NOT TARGET games-frame-pacing::swappy_static)
add_library(games-frame-pacing::swappy_static STATIC IMPORTED)
set_target_properties(games-frame-pacing::swappy_static PROPERTIES
    IMPORTED_LOCATION "C:/Users/alan_/.gradle/caches/transforms-3/467c3579f266553acb3501b86c7ede7f/transformed/games-frame-pacing-1.10.0/prefab/modules/swappy_static/libs/android.armeabi-v7a_API22_NDK23_cpp_shared_Release/libswappy.a"
    INTERFACE_INCLUDE_DIRECTORIES "C:/Users/alan_/.gradle/caches/transforms-3/467c3579f266553acb3501b86c7ede7f/transformed/games-frame-pacing-1.10.0/prefab/modules/swappy_static/include"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

