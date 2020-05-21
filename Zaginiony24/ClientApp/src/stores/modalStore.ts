import { RootStore } from "./rootStore";
import { observable, action } from "mobx";

export default class ModalStore{
    rootStore:RootStore;
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore
    }

    @observable.shallow modal = {
        open: false,
        body: null,
        actions: null
    }

    @action openModal = (content:any, actions:any) => {
        this.modal.open = true;
        this.modal.body = content;
        this.modal.actions = actions;
    }

    @action closeModal = () => {
        this.modal.open = false;
        this.modal.body = null;
    }
}