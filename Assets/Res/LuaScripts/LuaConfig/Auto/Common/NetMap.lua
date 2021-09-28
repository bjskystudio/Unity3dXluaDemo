

return _G.ConstClass("NetMap",{
    mail = { Sid = "mail", ConnectType = 1, IsPush = true, Parallel = false, Cmd = "mail", ClientPB = "无", ServerPB = "single_str" },
    mail_port_delete = { Sid = "mail_port_delete", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/mail_port/delete", ClientPB = "single_int", ServerPB = "s2c_mail" },
    mail_port_delete_all = { Sid = "mail_port_delete_all", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/mail_port/delete_all", ClientPB = "single_str", ServerPB = "s2c_mail_all" },
    mail_port_get_mail_red = { Sid = "mail_port_get_mail_red", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/mail_port/get_mail_red", ClientPB = "single_str", ServerPB = "single_int" },
    mail_port_get_mails = { Sid = "mail_port_get_mails", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/mail_port/get_mails", ClientPB = "single_str", ServerPB = "s2c_get_mails" },
    mail_port_read = { Sid = "mail_port_read", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/mail_port/read", ClientPB = "single_int", ServerPB = "s2c_mail" },
    mail_port_take = { Sid = "mail_port_take", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/mail_port/take", ClientPB = "single_int", ServerPB = "s2c_mail" },
    mail_port_take_all = { Sid = "mail_port_take_all", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/mail_port/take_all", ClientPB = "single_str", ServerPB = "s2c_mail_all" },
    role_port_check_name = { Sid = "role_port_check_name", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/check_name", ClientPB = "single_str", ServerPB = "single_str" },
    role_port_create_role = { Sid = "role_port_create_role", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/create_role", ClientPB = "c2s_create_role", ServerPB = "single_str" },
    role_port_get_random_name = { Sid = "role_port_get_random_name", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/get_random_name", ClientPB = "single_str", ServerPB = "s2c_get_random_name" },
    role_port_get_user = { Sid = "role_port_get_user", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/role_port/get_user", ClientPB = "single_str", ServerPB = "s2c_get_user" },
    scene_port_get_all_info = { Sid = "scene_port_get_all_info", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/scene_port/get_all_info", ClientPB = "single_str", ServerPB = "s2c_get_all_sprite" },
    scene_port_move = { Sid = "scene_port_move", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/scene_port/move", ClientPB = "c2s_move", ServerPB = "single_str" },
    scene_port_out = { Sid = "scene_port_out", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/scene_port/out", ClientPB = "single_str", ServerPB = "single_str" },
    scene_port_switch_scene = { Sid = "scene_port_switch_scene", ConnectType = 1, IsPush = false, Parallel = false, Cmd = "/game/scene_port/switch_scene", ClientPB = "c2s_switch_scene", ServerPB = "single_str" },
    scene_change = { Sid = "scene_change", ConnectType = 1, IsPush = true, Parallel = false, Cmd = "scene_change", ClientPB = "none", ServerPB = "p_scene_change" },
    scene_events = { Sid = "scene_events", ConnectType = 1, IsPush = true, Parallel = false, Cmd = "scene_events", ClientPB = "none", ServerPB = "p2c_scene_events" }
})