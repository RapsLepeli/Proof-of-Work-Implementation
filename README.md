# Proof-of-Work-Implementation
Honours Project - Implementation of the Proof of Work consensus algorithm.

## Objectives
This documentaion is used to track the development of a fully functional blockchain system. This will act as a guide and notes for the developer. This documentation might be used as a template for both the user manual and the technical manual. This documentation will be written in Markdown: Here's a link for the basic syntax [Markdown syntax](https://www.markdownguide.org/basic-syntax/).<br>
# Getting started
Visual Studio 2017 or 2019 can be used. At this point the application is untested on other development enriroments and fucses on windows systems. Below are the recomended system requiements.

- Windows 10 operating system
- Intel core I5 7th gen / AMD 2nd and 3rd Gen CPU’s
- 8 Gb DDR4 Ram
- 120Gb SSD
- Access to the internet 

1. <u>Downloading the repository:</u><br> 
To clone the repository,make sure git is installed and configures and  in command prompt,  `git clone --recursive https://github.com/RapsLepeli/Proof-of-Work-Implementation.git`

2. <u>Confugiring the dependencies</u><br>
open the solution file in Visual Studio. The minimum .Net SDK should be `v4.7.2`

3. ### User Guide: 
    - #### To be added as development carries on
 
# The Plan
  This project aims to implement a Blockchain Proof-of-Work (PoW) consensus algorithm. A simple blockchain system will be created from scratch to create, validate, and complete transactions. The blockchain system implemented will include a data structure to record the transactions, the PoW consensus algorithm, a transaction, and a simple peer-to-peer network that simulates nodes or computers participating in the transaction; this includes the sender, receiver, and block proposers. 
  
# Deliverables and Componets
These will change and evolve as time goes by
>	**The Distributed Blockchain data structure** can enable the untrusted network of participants to agree on a single transaction record. In addition, the data structure acts as a tamper-proof distributed ledger with cryptographically linked sequential blocks where each block contains a set of transaction data.
>
>	**A usable Transaction entity.** The transaction should be able to be recorded on the data structure. In this project, the transaction will be a simple virtual currency sent to other accounts participating in the system. 
>
>	**A simple functional Peer-to-Peer(P2P) network:** The system should generate a scalable network of virtual computer nodes participating in the blockchain system.
>
>	**A Functional Proof-of-Work Consensus algorithm.** The PoW algorithm should be able to verify transactions and add a new block on the blockchain on a timely basis that the tester or user will set. It should also be able to adjust the mining difficulty depending on how quickly participants add blocks. If mining is happening too fast, the hash computations get harder. If it is going too slowly, they get easier. It should also be able to allow block proposers to earn rewards after solving hash computations.
>
>	**A simple Minimal interface.** The interface should allow the user to set the number of network nodes, start a transaction and display notifications when transactions are completed or created.


## <u>The Distributed Blockchain data structure</u><br>

A distributed blockchain data structure. The blockchain data structure can be defined as an ordered, back-linked list of blocks containing transactions. It can be stored in a simple database or as a file. A hash-generated cryptographic hash technique on the block header identifies each block. For example, in the "prior block hash" field of the block header, each block refers to a preceding block called the parent block (Marin, 2018) . This ledger will be used to record all transactions in this system. The figure below shows what a general blockchain data structure looks like ![Basic Structure](/assets/Structureofblocksinblockchain.png)

Resources that will be used as references are listed below
- [Geeks for Geeks Java Implementation](https://www.geeksforgeeks.org/implementation-of-blockchain-in-java/?ref=rp)
- [Write your Own Proof-of-Work Blockchain](https://www.jmeiners.com/tiny-blockchain/)
- [Blockchain and Proof of Work](https://marceloh-web.medium.com/blockchain-and-proof-of-work-f35ffc33c459)
- [Building a Blockchain in Under 15 Minutes - Programmer explains](https://www.youtube.com/watch?v=baJYhYsHkLM)
- [Bitcoin ₿ in 100 Seconds // Build your Own Blockchain
](https://www.youtube.com/watch?v=qF7dkrce-mQ)
- [Build Blockchain from Scratch](https://youtube.com/playlist?list=PLCQ3cvOTrX6DlfYPigIw4QiNnE3wnoAxR)
- [Build Your Own Blockchain Using Python
](https://www.youtube.com/watch?v=Jt9MYcSsVzs)
- [Coding A Blockchain in Python
](https://www.youtube.com/watch?v=pYasYyjByKI)
- [Creating a blockchain with Javascript (Blockchain, part 1)](https://www.youtube.com/watch?v=zVqczFZr124)
- [Blockchain from Scratch in Python Tutorial
](https://www.youtube.com/watch?v=alNU9AVWkQk)
- [Build a Blockchain with Python & FastAPI
](https://www.youtube.com/watch?v=G5M4bsxR-7E)
- [Blockchain Programming Development with Python and Firebase
](https://www.youtube.com/watch?v=DFP9x5QS8tk)
- [Create a smple Blockchain  in Python](https://www.youtube.com/@SkoloOnline/search?query=blockchain)
- [Building a Blockchain in 15 Minutes (Python) - Programmer explains
](https://www.youtube.com/watch?v=4FwBB6vhilU&pp=ygUaY3JlYXRlIGEgc21wbGUgQmxvY2tjaGFpbiA%3D)
- [Simple Blockchain in 5 Minutes
](https://www.youtube.com/watch?v=MViBvQXQ3mM)