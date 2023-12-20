import {
    ExpandLess,
    ExpandMore
} from "@mui/icons-material";
import {
    Box,
    Collapse,
    Divider,
    List,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    Theme
} from "@mui/material";
import {useContext, useState} from "react";
import {list} from "./ListPage.tsx";
import {SxProps} from "@mui/system/styleFunctionSx";
import {UserContext} from "../context/UserContextProvider.tsx";

interface StateSubMenu {
    [key: string]: boolean;
}

interface NavlistDrawerProps {
    sx?: SxProps<Theme>;
}

function NavlistDrawer({sx}: NavlistDrawerProps) {
    
    const [subMenuState, setSubMenuState] = useState<StateSubMenu>({}); // Estado para controlar el colapso de submenús
    const {user} = useContext(UserContext);
    
    const toggleSubMenu = (itemName: string) => {
        setSubMenuState((prevState) => ({
            ...prevState,
            [itemName]: !prevState[itemName],
        }));
    };
    
    return (
        <Box sx={sx}>
            <List>
                {list.map((item) => {
                    
                    if (item.authRequired && !user) {
                        return null;
                    }
                    
                    if (item.subMenu) {
                        
                        return (
                            <div key={item.name}>
                                <ListItemButton onClick={() => toggleSubMenu(item.name)}>
                                    
                                    <ListItemIcon>
                                        {item.icon}
                                    </ListItemIcon>
                                    
                                    <ListItemText primary={item.name}/>
                                    
                                    {subMenuState[item.name] ? <ExpandLess/> : <ExpandMore/>}
                                
                                </ListItemButton>
                                
                                <Collapse in={subMenuState[item.name]} timeout="auto" unmountOnExit>
                                    
                                    <List component="div" disablePadding>
                                        {item.subMenu.map((subItem) => {
                                            
                                            if (subItem.authRequired && !user) {
                                                return null;
                                            }
                                            
                                            return (
                                                <ListItemButton key={subItem.name} sx={{pl: 4}} href={subItem.path}>
                                                    <ListItemText primary={subItem.name}/>
                                                </ListItemButton>
                                            )
                                            
                                        })}
                                    </List>
                                
                                </Collapse>
                            </div>
                        );
                        
                    } else {
                        return (
                            <ListItemButton key={item.name} href={item.path}>
                                
                                <ListItemIcon>
                                    {item.icon}
                                </ListItemIcon>
                                
                                <ListItemText primary={item.name}/>
                            </ListItemButton>
                        );
                    }
                })}
                <Divider/>
            </List>
        </Box>
    )
}

export default NavlistDrawer;