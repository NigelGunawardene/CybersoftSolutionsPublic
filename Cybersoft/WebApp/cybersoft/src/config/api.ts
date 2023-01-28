import { environment } from 'src/environments/environment'

export const baseUrl = environment.production ? 'https://prodUrl' : 'http:localhost/3000'
export const productsUrl = baseUrl + '/products'
export const cartUrl = baseUrl + '/cart'
