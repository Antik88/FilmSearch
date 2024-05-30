import { makeAutoObservable } from "mobx"

export default class FilmStore {
    constructor() {
        this._genres = []
        this._films = []
        this._actors = [] 
        this._searchName = ''
        this._searchYear = ''
        this._searchGenres = [] 
        makeAutoObservable(this)
    }

    setGenres(genres) {
        this._genres = genres 
    }

    setFilms(films) {
        this._films = films 
    }

    setActors(actors) {
        this._actors = actors 
    }

    setSearchName(searchName) {
        this._searchName = searchName  
    }

    setSearchYear(searchYear) {
        this._searchYear = searchYear  
    }

    setSearchGenres(searchGenres) {
        this._searchGenres = searchGenres  
    }

    get genres() {
        return this._genres
    }

    get films() {
        return this._films
    }

    get actors() {
        return this._actors
    }

    get searchName() {
        return this._searchName
    }

    get searchYear() {
        return this._searchYear
    }
    
    get searchGenres() {
        return this._searchGenres
    }
}
