import { Order } from "./order"
import { Restaurant } from "./restaurant"

export interface User {
    id: string,
    username: string,
    firstName: string,
    lastName:string,
    token: string,
    type: string,
}

export interface Admin extends User{
    restaurants: [Restaurant]
}

export interface Customer extends User{
    orders : [Order]
}