# PartialsCiphers

`PartialsCiphers` is a lightweight Unity package containing experimental text obfuscation ciphers designed around fixed-length symbolic encoding and key-based shifting.

Inside the package live two ciphers, each with its own encoding density and transformation style, like two different lock mechanisms built from the same strange metal.

---

# Included Ciphers

## `Symb4-wk`

A fixed-width cipher that transforms every supported character, including uppercase and lowercase letters, into a unique 4-symbol sequence.

After encoding, the generated sequence is shifted using a key-driven transformation layer.

### Features

* Fixed 4-symbol output blocks
* Supports uppercase and lowercase characters
* Key-based shifting stage
* Deterministic encoding and decoding
* Designed for compact symbolic obfuscation workflows

### Workflow

1. Input text is converted into mapped 4-symbol sequences
2. The encoded stream is shifted using the provided key
3. Decoding reverses the shift and restores the original text

---

## `Glyph3-wk`

A denser variation of the same concept.

`Glyph3-wk` replaces characters with fixed 3-symbol sequences before applying the key-based shift layer.

Compared to `Symb4-wk`, this cipher produces shorter encoded output while preserving the same general transformation pipeline.

### Features

* Fixed 3-symbol output blocks
* Uppercase and lowercase character support
* Key-driven shift transformation
* Deterministic encode/decode process
* Reduced encoded length compared to `Symb4-wk`

### Workflow

1. Characters are converted into 3-symbol encoded blocks
2. Encoded output is shifted using a key
3. Decoding restores the original sequence

---

# Unity Installation

## Via Package Manager (Git URL)

Open Unity Package Manager and add the package using a Git URL:

```text
https://github.com/yourname/PartialsCiphers.git
```

---

# Package Structure

```text
PartialsCiphers/
├── Runtime/
├── Editor/
├── Samples~
├── Documentation~
└── package.json
```

---

# Goals

`PartialsCiphers` is intended for:

* Experimental encoding systems
* Lightweight text obfuscation
* Puzzle systems
* ARG-style mechanics
* Stylized data transformation
* Learning projects involving custom cipher pipelines

It is not intended for real-world cryptographic security.

---

# License

MIT License

---

# Notes

Both ciphers use fixed-length symbolic substitution followed by key-based shifting, but differ in output density and representation strategy.

Choose:

* `Symb4-wk` for wider symbolic encoding
* `Glyph3-wk` for denser encoded streams

Like choosing between two labyrinths where one has longer corridors and the other hides tighter corners.
