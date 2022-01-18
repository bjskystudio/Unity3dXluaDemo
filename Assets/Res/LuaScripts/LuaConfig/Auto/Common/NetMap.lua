

return _G.ConstClass("NetMap",{
    login_port_login = { Sid = "login_port_login", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/login_port/login", ClientPB = "c2s_login", ServerPB = "s2c_login" },
    role_port_check_name = { Sid = "role_port_check_name", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/check_name", ClientPB = "single_str", ServerPB = "single_str" },
    role_port_create_role = { Sid = "role_port_create_role", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/create_role", ClientPB = "c2s_create_role", ServerPB = "single_str" },
    role_port_get_avatar_border = { Sid = "role_port_get_avatar_border", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/get_avatar_border", ClientPB = "single_str", ServerPB = "s2c_get_avatar_border" },
    role_port_get_random_name = { Sid = "role_port_get_random_name", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/get_random_name", ClientPB = "single_str", ServerPB = "s2c_get_random_name" },
    role_port_get_update_rolename_times = { Sid = "role_port_get_update_rolename_times", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/get_update_rolename_times", ClientPB = "single_str", ServerPB = "single_int" },
    role_port_get_user = { Sid = "role_port_get_user", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/get_user", ClientPB = "single_str", ServerPB = "s2c_get_user" },
    role_port_role_state = { Sid = "role_port_role_state", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/role_state", ClientPB = "single_int", ServerPB = "single_str" },
    role_port_update_avatar_border = { Sid = "role_port_update_avatar_border", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/update_avatar_border", ClientPB = "single_int", ServerPB = "single_str" },
    role_port_update_rolename = { Sid = "role_port_update_rolename", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/update_rolename", ClientPB = "single_str", ServerPB = "s2c_update_rolename" },

})